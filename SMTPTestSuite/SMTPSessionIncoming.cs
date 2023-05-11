/*
 * By David Barrett, Microsoft Ltd. 2018 - 2023. Use at your own risk.  No warranties are given.
 * 
 * DISCLAIMER:
 * THIS CODE IS SAMPLE CODE. THESE SAMPLES ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY KIND.
 * MICROSOFT FURTHER DISCLAIMS ALL IMPLIED WARRANTIES INCLUDING WITHOUT LIMITATION ANY IMPLIED WARRANTIES OF MERCHANTABILITY OR OF FITNESS FOR
 * A PARTICULAR PURPOSE. THE ENTIRE RISK ARISING OUT OF THE USE OR PERFORMANCE OF THE SAMPLES REMAINS WITH YOU. IN NO EVENT SHALL
 * MICROSOFT OR ITS SUPPLIERS BE LIABLE FOR ANY DAMAGES WHATSOEVER (INCLUDING, WITHOUT LIMITATION, DAMAGES FOR LOSS OF BUSINESS PROFITS,
 * BUSINESS INTERRUPTION, LOSS OF BUSINESS INFORMATION, OR OTHER PECUNIARY LOSS) ARISING OUT OF THE USE OF OR INABILITY TO USE THE
 * SAMPLES, EVEN IF MICROSOFT HAS BEEN ADVISED OF THE POSSIBILITY OF SUCH DAMAGES. BECAUSE SOME STATES DO NOT ALLOW THE EXCLUSION OR LIMITATION
 * OF LIABILITY FOR CONSEQUENTIAL OR INCIDENTAL DAMAGES, THE ABOVE LIMITATION MAY NOT APPLY TO YOU.
 * */

using System;
using System.Net.Sockets;
using System.Net.Security;
using System.IO;
using Logging;
using System.Runtime.InteropServices.ComTypes;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using System.Threading;

namespace SMTPTestSuite
{
    public class SMTPSessionIncoming
    {
        Socket _socket;
        private string _sessionProtocol = "";
        private string _rejectMessage = String.Empty; // If not empty, all messages are rejected with this message
        private int _rejectStage = 1; // The stage at which message is rejected (1 = RCPT TO)
        Logging.Logger _Logger = null;
        private bool _requireAUTH = false;
        private bool _authed = false;
        private string _storeFolder = "";
        private bool _useRecipientSubfolders = true;
        List<string> _recipients = new List<string>();
        private string _mailFrom = "";
        private string _clientIdentifier = "";
        private bool _8bitMime = false;
        private bool _dataCommandSent = false;
        static int _lastSessionNumber = 0;
        private int _sessionNumber = -1;
        private X509Certificate _tlsCertificate = null;
        private List<string> _receiveForDomains = new List<string>();
        private Validators.Connection _connectionValidator = null;

        private SslStream _sslStream = null;
        private NetworkStream _networkStream = null;
        private DateTime _sessionStartTime = DateTime.Now;
        private int _timeoutInSeconds = 15;
        private int _maximumMessageSize = 1024 * 1024 * 5;

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="socket"></param>
        public SMTPSessionIncoming(Socket socket, Logger logger, bool RequireAuth)
        {
            _socket = socket;
            _Logger = logger;
            _authed = false;
            _requireAUTH = RequireAuth;
            _lastSessionNumber++;
            _sessionNumber = _lastSessionNumber;
        }

        public SMTPSessionIncoming(Socket socket, Logger logger, bool RequireAuth, string MessageStoreFolder, bool CreateRecipientSubfolders = true)
            : this(socket, logger, RequireAuth)
        {
            _storeFolder = MessageStoreFolder;
            _useRecipientSubfolders = CreateRecipientSubfolders;
        }

        public X509Certificate TLSCertificate
        {
            get { return _tlsCertificate; }
            set { _tlsCertificate = value; }
        }

        /// <summary>
        /// Returns the session protocol log
        /// </summary>
        public string SessionProtocolLog
        {
            get { return _sessionProtocol; }
            set { _sessionProtocol = value; }
        }

        /// <summary>
        /// Get or set the reject message
        /// </summary>
        public string RejectMessage
        {
            get { return _rejectMessage; }
            set { _rejectMessage = value; }
        }

        internal Validators.Connection ConnectionValidator
        {
            get { return _connectionValidator; }
            set { _connectionValidator = value; }
        }

        /// <summary>
        /// Write the given data to log
        /// </summary>
        /// <param name="LogItem">Data to log</param>
        private void Log(string LogItem)
        {
            if (_Logger == null) return;
            _Logger.Log($"{_sessionNumber}:{LogItem.Trim()}");
        }

        /// <summary>
        /// Read pending data from SSL stream
        /// </summary>
        /// <param name="SuppressLog">Whether to suppress write to log file</param>
        /// <returns>Data as string</returns>
        private string ReadSSLStream(bool SuppressLog = false)
        {
            byte[] buffer = new byte[1024*10];
            int bytes = buffer.Length;
            StringBuilder response = new StringBuilder();
            while (bytes == buffer.Length)
            {
                bytes = _sslStream.Read(buffer, 0, buffer.Length);
                if (_8bitMime && _dataCommandSent)
                    response.Append(System.Text.Encoding.UTF8.GetString(buffer, 0, bytes));
                else
                    response.Append(System.Text.Encoding.ASCII.GetString(buffer, 0, bytes));
            }
            if (!SuppressLog)
                Log($"*--> {response}");
            return response.ToString();
        }

        /// <summary>
        /// Write the given data to the SSL stream
        /// </summary>
        /// <param name="Data">Data to write</param>
        private void WriteSSLStream(string Data, bool SuppressErrors = false)
        {
            try
            {
                _sslStream.Write(System.Text.Encoding.ASCII.GetBytes(Data));
                _sslStream.Flush();
                Log($"<--* {Data}");
            }
            catch (Exception ex)
            {
                if (!SuppressErrors)
                    Log($"Error writing network stream: {ex.Message}");
            }
        }

        /// <summary>
        /// Read pending data from network stream (or SSL stream if active)
        /// </summary>
        /// <param name="SuppressLog">Whether to suppress write to log file</param>
        /// <returns>Data as string</returns>
        private string ReadNetworkStream(bool SuppressLog = false)
        {
            if (_sslStream != null)
                return ReadSSLStream(SuppressLog);

            DateTime waitForCommandStart = DateTime.Now;
            while (!_networkStream.DataAvailable && DateTime.Now.Subtract(waitForCommandStart).TotalSeconds < 10)
                Thread.Sleep(50);
            if (!_networkStream.DataAvailable)
                return null;

            int bytes = -1;
            byte[] buffer = new byte[4096];
            StringBuilder response = new StringBuilder();
            while (_networkStream.DataAvailable)
            {
                bytes = _networkStream.Read(buffer, 0, buffer.Length);
                if (_8bitMime && _dataCommandSent)
                    response.Append(System.Text.Encoding.UTF8.GetString(buffer, 0, bytes));
                else
                    response.Append(System.Text.Encoding.ASCII.GetString(buffer, 0, bytes));
            }
            if (!SuppressLog)
                Log($"--> {response.ToString()}");
            return response.ToString();
        }

        /// <summary>
        /// Write the given data to the network stream (or SSL stream if active)
        /// </summary>
        /// <param name="Data">Data to write</param>
        private void WriteNetworkStream(string Data, bool SuppressErrors = false)
        {
            if (!Data.EndsWith(Environment.NewLine))
                Data = $"{Data}{Environment.NewLine}";
            if (_sslStream != null)
            {
                WriteSSLStream(Data, SuppressErrors);
                return;
            }

            byte[] byteData = System.Text.Encoding.ASCII.GetBytes(Data);
            try
            {
                _networkStream.Write(byteData, 0, byteData.Length);
                _networkStream.Flush();
                Log($"<-- {Data}");
            }
            catch (Exception ex)
            {
                if (!SuppressErrors)
                    Log($"Error writing network stream: {ex.Message}");
            }
        }

        /// <summary>
        /// Perform any necessary processing for RCPTTO
        /// </summary>
        /// <param name="RCPTTO">The full RCPT TO command to process</param>
        private bool ProcessRCPTTO(string RCPTTO)
        {
            if (!String.IsNullOrEmpty(_rejectMessage) && (_rejectStage == 1))
            {
                // Reject
                WriteNetworkStream(_rejectMessage);
                return true;
            }
            string recipient = RCPTTO.Substring(8).Trim();
            if (recipient.StartsWith("<"))
                recipient = recipient.Substring(1);
            if (recipient.EndsWith(">"))
                recipient = recipient.Substring(0, recipient.Length - 1);

            if (_receiveForDomains.Count>0)
            {
                bool domainOk = false;
                foreach (string domain in _receiveForDomains)
                    if (recipient.ToLower().EndsWith($"@{domain}"))
                    {
                        domainOk = true;
                        break;
                    }
                if (!domainOk)
                {
                    WriteNetworkStream("550 Recipient not valid");
                    return false;
                }
            }

            WriteNetworkStream("250 Recipient OK");
            _recipients.Add(recipient);
            return true;
        }

        private string ParseCommandParameter(string command, string parameter)
        {
            int paramIndex = command.IndexOf($"{parameter}= ");
            if (paramIndex > 0)
            {
                // Read parameter
                string param = command.Substring(paramIndex);
                int paramEnd = command.IndexOf(" ", paramIndex);
                if (paramEnd > 0)
                    param = param.Substring(0, paramEnd);
                return param;
            }
            return null;
        }

        private bool ProcessMAILFROM(string MAILFROM)
        {
            if (_requireAUTH && !_authed)
            {
                WriteNetworkStream("530 5.7.1 Client didn't authenticate");
                return true;
            }

            _mailFrom = GetEmailAddressFromMAILFROM(MAILFROM.Substring(10).Trim());
            string mailSize = ParseCommandParameter(MAILFROM, "SIZE");
            int messageSize;
            if (int.TryParse(mailSize, out messageSize))
                if (messageSize > _maximumMessageSize)
                {
                    WriteNetworkStream("552 message exceeds fixed maximum message size");
                    return true;
                }

            string mailFromBody = ParseCommandParameter(MAILFROM, "BODY");
            if (mailFromBody == "BITMIME")
                _8bitMime = true;

            WriteNetworkStream("250 Sender OK");
            return true;
        }

        private void ShowHelp()
        {
            WriteNetworkStream($"250-{SmtpMock.ServerName}");
            WriteNetworkStream("250-AUTH LOGIN PLAIN");
            WriteNetworkStream($"250-SIZE {_maximumMessageSize}");
            WriteNetworkStream("250-8BITMIME");
            //WriteNetworkStream("250-CHUNKING");
            //WriteNetworkStream("250-BINARYMIME");
            if (_tlsCertificate != null && _sslStream == null)
                WriteNetworkStream("250-STARTTLS");
            WriteNetworkStream("250 ENHANCEDSTATUSCODES");
        }

        private void ProcessEHLO(string EHLO, bool helo = false)
        {
            EHLO = EHLO.TrimEnd();
            int idStart = EHLO.IndexOf(" ");
            if (idStart<0 || idStart==EHLO.Length)
            {
                // Invalid EHLO
                WriteNetworkStream("501 5.5.4 Missing identification");
                return;
            }
            _clientIdentifier = EHLO.Substring(idStart + 1);
            if (helo)
                WriteNetworkStream($"250 Hello {((System.Net.IPEndPoint)_socket.RemoteEndPoint).Address} ({_clientIdentifier})");
            else
                ShowHelp();
        }

        /// <summary>
        /// Handle incoming SMTP session
        /// </summary>
        public void Process()
        {
            _networkStream = new NetworkStream(_socket);
            DateTime lastDataReceived = DateTime.Now;

            bool clientWasValid = false;
            int invalidCommandCount = 0;
            int emptyCommandCount = 0;
            try
            {
                WriteNetworkStream($"220 {SmtpMock.ServerName} Ready");
                string sDATA = "";

                while (_socket.Connected == true)
                {
                    if (_networkStream == null) break;

                    string receivedData = String.Empty;
                    try
                    {
                        receivedData = ReadNetworkStream(_dataCommandSent);
                    }
                    catch { }

                    if (String.IsNullOrEmpty(receivedData))
                    {
                        emptyCommandCount++;
                        if (emptyCommandCount > 2)
                        {
                            WriteNetworkStream("500 5.5.2 Too many empty commands, closing connection");
                            break;
                        }
                        if (DateTime.Now.Subtract(lastDataReceived).TotalSeconds > _timeoutInSeconds)
                        {
                            Log($"Connection idle for longer than {_timeoutInSeconds} seconds; closing");
                            clientWasValid = false;
                            break;
                        }
                        continue;
                    }
                    else
                        lastDataReceived = DateTime.Now;
                    
                    _sessionProtocol += receivedData;

                    if (!_dataCommandSent)
                    {
                        List<string> lines = receivedData.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).ToList<string>();
                        if (lines.Count > 1)
                            Log($"{lines.Count} lines received");
                    }

                    string command = receivedData.TrimEnd();
                    if (_dataCommandSent)
                    {
                        sDATA += receivedData;
                        if (receivedData.EndsWith("\r\n.\r\n"))
                        {
                            _dataCommandSent = false;
                            sDATA = sDATA.Substring(0, sDATA.Length - 5);
                            if (_sslStream != null)
                                Log($"*--> DATA: {sDATA.Length} bytes received");
                            else
                                Log($"--> DATA: {sDATA.Length} bytes received");
                            WriteNetworkStream("250 OK");
                            SaveMessage(sDATA);
                            clientWasValid = true;
                        }
                    }
                    else if (command.Equals("QUIT"))
                    {
                        WriteNetworkStream("221 2.0.0 Goodbye", true);
                        break;

                    }
                    else if (command.Equals("RSET"))
                    {
                        _mailFrom = "";
                        _recipients.Clear();
                        _authed = false;
                        WriteNetworkStream("250 Session reset");
                    }
                    else if (command.Equals("NOOP"))
                    {
                        WriteNetworkStream("250 OK");
                    }
                    else if (command.Equals("DATA"))
                    {
                        _dataCommandSent = true;
                        WriteNetworkStream("354 Ready for data, terminate with <CR>.<CR>");
                        sDATA = "";
                    }
                    else if (command.StartsWith("BDAT"))
                    {
                        WriteNetworkStream("502 NOT IMPLEMENTED");
                        sDATA = "";
                    }
                    else if (command.StartsWith("EHLO"))
                        ProcessEHLO(receivedData);
                    else if (command.StartsWith("HELO"))
                    {
                        ProcessEHLO(receivedData, true);
                    }
                    else if (command.StartsWith("AUTH"))
                    {
                        if (_tlsCertificate != null && _sslStream == null)
                        {
                            WriteNetworkStream("538 5.7.11 Use STARTTLS first");
                        }
                        else
                        {
                            _authed = smtpAUTH(receivedData.ToUpper().Substring(receivedData.IndexOf(" ") + 1));
                            if (!_authed)
                                break;
                        }
                    }
                    else if (command.StartsWith("MAIL FROM"))
                        clientWasValid = ProcessMAILFROM(receivedData);
                    else if (command.StartsWith("RCPT TO"))
                        clientWasValid = clientWasValid && ProcessRCPTTO(receivedData);
                    else if (command.Equals("STARTTLS"))
                    {
                        if (_tlsCertificate != null)
                        {
                            WriteNetworkStream("220 Starting TLS");
                            _sslStream = new SslStream(_networkStream);
                            _sslStream.AuthenticateAsServer(_tlsCertificate, false, false);
                        }
                        else
                            WriteNetworkStream("502 Not implemented");
                    }
                    else if (command.Equals("HELP"))
                        ShowHelp();
                    else
                    {
                        //Log($"Invalid command: {command}");
                        invalidCommandCount++;
                        if (invalidCommandCount>2)
                        {
                            WriteNetworkStream("500 5.5.2 Too many unrecognised commands, closing connection");
                            break;
                        }
                        WriteNetworkStream("500 5.5.2 Syntax error, command unrecognised");
                        _timeoutInSeconds = 5;
                    }
                }
            }
            catch (Exception exception)
            {
                Log("ERROR: " + exception.Message);
            }
            finally
            {
                if (!clientWasValid)
                    _connectionValidator?.LogBadBehaviour((System.Net.IPEndPoint)_socket.RemoteEndPoint);

                if (_sslStream != null)
                {
                    _sslStream.Close();
                    _sslStream.Dispose();
                    _sslStream = null;
                }
                _networkStream.Close();
                _networkStream.Dispose();
                _socket.Close();
            }
            Log($"Session finished in {DateTime.Now.Subtract(_sessionStartTime)}, {emptyCommandCount} empty command(s)");
        }

        private string GetEmailAddressFromMAILFROM(string MAILFROM)
        {
            int iEmailStart = MAILFROM.IndexOf("<") + 1;
            int iEmailEnd = MAILFROM.IndexOf(">", iEmailStart);

            if (iEmailStart < 0 || iEmailEnd < 0)
                return "Unknown";

            return MAILFROM.Substring(iEmailStart, iEmailEnd - iEmailStart);
        }

        bool smtpAUTHPLAIN(string AuthType)
        {
            // SMTP PLAIN authentication
            string sAuthInfo = "";
            if (AuthType.Contains(" "))
            {
                sAuthInfo=AuthType.Substring(AuthType.IndexOf(" ")+1);
            }
            if (String.IsNullOrEmpty(sAuthInfo))
            {
                WriteNetworkStream("535 5.7.1 Authentication failed");
                return false;
            }

            sAuthInfo = Base64Decode(sAuthInfo);
            string[] aAuth = sAuthInfo.Split('\0');
            if (String.IsNullOrEmpty(aAuth[0]) || String.IsNullOrEmpty(aAuth[2]))
            {
                WriteNetworkStream("535 5.7.1 Authentication failed");
                return false;
            }
            Log("AuthId=" + aAuth[0] + "; UserId=" + aAuth[1] + "; Password=" + aAuth[2]);
            WriteNetworkStream("235 Authentication complete");
            return true;
        }

        bool smtpAUTHLOGIN(string AuthType)
        {
            // SMTP AUTH authentication
            int iStage = 0;
            string sUsername = "";
            string sPassword = "";
            WriteNetworkStream($"334 {Base64Encode("Username:")}");
            DateTime lastDataReceived = DateTime.Now;

            while (_socket.Connected == true)
            {
                string line = ReadNetworkStream();
                if (String.IsNullOrEmpty(line))
                {
                    if (DateTime.Now.Subtract(lastDataReceived).TotalSeconds > _timeoutInSeconds)
                    {
                        Log($"Authentication idle for longer than {_timeoutInSeconds} seconds");
                        WriteNetworkStream("535 5.7.1 Authentication failed");
                        return false;
                    }
                    continue;
                }

                if (line.StartsWith("QUIT"))
                {
                    WriteNetworkStream("221 Service closing transmission channel");
                    return false;
                }
                else if (iStage == 0)
                {
                    // This is the username
                    sUsername = Base64Decode(line);
                    WriteNetworkStream($"334 {Base64Encode("Password:")}");
                    iStage++;
                }
                else
                {
                    // This is the password
                    sPassword = Base64Decode(line);
                    break;
                }

            }

            Log("Username = " + sUsername + "; Password = " + sPassword);
            if (String.IsNullOrEmpty(sUsername) || String.IsNullOrEmpty(sPassword))
            {
                WriteNetworkStream("535 5.7.1 Authentication failed");
                return false;
            }
            WriteNetworkStream("235 Authentication complete");
            return true;
        }

        bool smtpAUTH(string AuthType)
        {
            // SMTP AUTH implementation

            if (_authed)
            {
                WriteNetworkStream("503 5.0.3 Already authenticated");
                return false;
            }

            if (AuthType.StartsWith("LOGIN")) return smtpAUTHLOGIN(AuthType);
            if (AuthType.StartsWith("PLAIN")) return smtpAUTHPLAIN(AuthType);

            WriteNetworkStream("535 5.7.1 Authentication failed");
            return false;
        }

        string Base64Encode(string plainText)
        {
            byte[] bytesToEncode = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(bytesToEncode);
        }

        string Base64Decode(string base64Text)
        {
            byte[] decodedBytes = Convert.FromBase64String(base64Text);
            return System.Text.Encoding.UTF8.GetString(decodedBytes);
        }

        public void Quit()
        {
            _socket.Close();
        }

        private string FormatHeader(string header)
        {
            if (header.Length < 78)
                return header;

            StringBuilder newHeader = new StringBuilder();
            int spacePos = header.IndexOf(" ", 16);
            int lastSpacePos=0;
            int copyFrom = 0;
            while (lastSpacePos < header.Length)
            {
                while (spacePos > 0 && spacePos < 78+copyFrom)
                {
                    lastSpacePos = spacePos;
                    spacePos = header.IndexOf(" ", spacePos + 1);
                    if (spacePos < 0)
                        lastSpacePos = header.Length;
                }
                if (newHeader.Length > 0)
                    newHeader.Append(" ");
                newHeader.AppendLine(header.Substring(copyFrom, lastSpacePos-copyFrom));
                copyFrom = lastSpacePos + 1;
            }
            return newHeader.ToString().TrimEnd();
        }

        private void SaveMessageToFile(string message, string file)
        {
            string receivedStamp = $"Received: from {((System.Net.IPEndPoint)_socket.RemoteEndPoint).Address} by {SmtpMock.ServerName}; {DateTime.UtcNow.ToString("r")}";
            receivedStamp = FormatHeader(receivedStamp);

            Log("Saving message to: " + file);
            try
            {
                StreamWriter oWriter = File.AppendText(file);
                oWriter.WriteLine(receivedStamp);
                oWriter.Write(message);
                oWriter.Close();
            }
            catch (Exception ex)
            {
                Log("Error while saving message: " + ex.Message);
            }

        }

        private void SaveMessage(string DATA)
        {
            if (String.IsNullOrEmpty(_storeFolder))
                return;

            string sFileName = _mailFrom + " " + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".eml";
            if (!_useRecipientSubfolders)
            {
                sFileName = _storeFolder + sFileName;
                SaveMessageToFile(DATA, sFileName);
                return;
            }

            foreach (string recipient in _recipients)
            {
                string folderPath = $"{_storeFolder}{recipient}\\";
                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);
                sFileName = $"{folderPath}{sFileName}";
                SaveMessageToFile(DATA, sFileName);
            }
        }
    }
}
