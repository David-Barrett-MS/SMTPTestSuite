/*
 * By David Barrett, Microsoft Ltd. 2018. Use at your own risk.  No warranties are given.
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
using System.IO;
using Logging;

namespace SMTPTestSuite
{
    public class SMTPSessionIncoming
    {
        Socket _socket;
        private string _sessionProtocol = "";
        private string _rejectMessage = String.Empty; // If not empty, all messages are rejected with this message
        private int _rejectStage = 1; // The stage at which message is rejected (1 = RCPT TO)
        Logging.Logger _Logger = null;
        bool _requireAUTH = false;
        bool _authed = false;
        string _storeFolder = "";


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
        }

        public SMTPSessionIncoming(Socket socket, Logger logger, bool RequireAuth, string MessageStoreFolder)
            : this(socket, logger, RequireAuth)
        {
            _storeFolder = MessageStoreFolder;
        }

        /// <summary>
        /// session protocol 
        /// </summary>
        public string SessionProtocol
        {
            get { return _sessionProtocol; }
            set { _sessionProtocol = value; }
        }

        public string RejectMessage
        {
            get { return _rejectMessage; }
            set { _rejectMessage = value; }
        }


        private void Log(string LogItem)
        {
            if (_Logger == null) return;
            _Logger.Log(LogItem);
        }


        /// <summary>
        /// process session
        /// </summary>
        public void Process()
        {
            NetworkStream networkStream = new NetworkStream(_socket);
            StreamWriter streamWriter = new StreamWriter(networkStream);
            StreamReader streamReader = new StreamReader(networkStream);
            streamWriter.AutoFlush = true;

            try
            {
                streamWriter.WriteLine("220 SMTP Mock Server Ready");
                Log("--> 220 SMTP Mock Server Ready");
                bool datasent = false;
                string sDATA = "";
                string sMAILFROM = "";

                while (_socket.Connected == true)
                {
                    if (streamReader == null) break;

                    string line = null;
                    try
                    {
                        line = streamReader.ReadLine();
                    }
                    catch { }
                    if (line == null)
                        continue;
                    Log("<-- " + line);

                    _sessionProtocol += line + "\n";

                    if (line.ToUpper().StartsWith("QUIT"))
                    {
                        streamWriter.WriteLine("221 Service closing transmission channel");
                        Log("--> 221 Service closing transmission channel");
                        break;

                    }

                    if (line.ToUpper().StartsWith("DATA"))
                    {
                        datasent = true;
                        streamWriter.WriteLine("354 Ready for data, terminate with <CR>.<CR>");
                        Log("--> 354 Ready for data, terminate with <CR>.<CR>");
                        sDATA = "";
                    }
                    else if (datasent)
                    {
                        if (line.Trim() == ".")
                        {
                            datasent = false;
                            streamWriter.WriteLine("250 OK");
                            Log("--> 250 OK");
                            SaveMessage(sDATA, sMAILFROM);
                        }
                        else
                        {
                            sDATA += line + Environment.NewLine;
                        }
                    }
                    else if (line.ToUpper().StartsWith("EHLO"))
                    {
                        streamWriter.WriteLine("250-smtp.mock.local");
                        Log("--> 250-smtp.mock.local");
                        streamWriter.WriteLine("250-AUTH LOGIN PLAIN");
                        Log("--> 250-AUTH LOGIN PLAIN");
                        streamWriter.WriteLine("250-SIZE 26214400");
                        Log("--> 250-SIZE 26214400");
                        streamWriter.WriteLine("250 ENHANCEDSTATUSCODES");
                        Log("--> 250 ENHANCEDSTATUSCODES");
                    }
                    else if (line.ToUpper().StartsWith("AUTH"))
                    {
                        _authed=smtpAUTH(line.ToUpper().Substring(line.IndexOf(" ")+1),streamReader, streamWriter);
                    }
                    else if (line.ToUpper().StartsWith("MAIL FROM"))
                    {
                        if (_requireAUTH && !_authed)
                        {
                            streamWriter.WriteLine("530 5.7.1 Client didn't authenticate");
                            Log("--> 530 5.7.1 Client didn't authenticate");
                        }
                        else
                        {
                            streamWriter.WriteLine("250 Sender OK");
                            sMAILFROM = GetEmailAddressFromMAILFROM(line.Substring(10).Trim());
                            Log("--> 250 Sender OK");
                        }
                    }
                    else if (line.ToUpper().StartsWith("RCPT TO"))
                    {
                        if ( !String.IsNullOrEmpty(_rejectMessage) && (_rejectStage==1) )
                        {
                            // Reject
                            streamWriter.WriteLine(_rejectMessage);
                            Log("--> " + _rejectMessage);
                        }
                        else
                        {
                            streamWriter.WriteLine("250 Recipient OK");
                            Log("--> 250 Recipient OK");
                        }
                    }
                    else if (!datasent)
                    {
                        streamWriter.WriteLine("250 OK");
                        Log("--> 250 OK");
                    }
                }
            }
            catch (Exception exception)
            {
                Log("ERROR: " + exception.Message);
            }
            finally
            {
                streamReader.Close();
                streamWriter.Close();
                networkStream.Close();
                _socket.Close();
            }

        }

        private string GetEmailAddressFromMAILFROM(string MAILFROM)
        {
            int iEmailStart = MAILFROM.IndexOf("<") + 1;
            int iEmailEnd = MAILFROM.IndexOf(">", iEmailStart);

            if (iEmailStart < 0 || iEmailEnd < 0)
                return "Unknown";

            return MAILFROM.Substring(iEmailStart, iEmailEnd - iEmailStart);
        }

        bool smtpAUTHPLAIN(string AuthType, StreamReader streamReader, StreamWriter streamWriter)
        {
            // SMTP PLAIN authentication
            string sAuthInfo = "";
            if (AuthType.Contains(" "))
            {
                sAuthInfo=AuthType.Substring(AuthType.IndexOf(" ")+1);
            }
            if (String.IsNullOrEmpty(sAuthInfo))
                return false;

            sAuthInfo = Base64Decode(sAuthInfo);
            string[] aAuth = sAuthInfo.Split('\0');
            if (String.IsNullOrEmpty(aAuth[0]) || String.IsNullOrEmpty(aAuth[2]))
            {
                streamWriter.WriteLine("535 5.7.1 Authentication failed");
                Log("--> 535 5.7.1 Authentication failed");
                return false;
            }
            Log("AuthId=" + aAuth[0] + "; UserId=" + aAuth[1] + "; Password=" + aAuth[2]);
            streamWriter.WriteLine("235 Authentication complete");
            Log("--> 235 2.7.0 Authentication complete");
            return true;
        }

        bool smtpAUTHLOGIN(string AuthType, StreamReader streamReader, StreamWriter streamWriter)
        {
            // SMTP AUTH authentication
            int iStage = 0;
            string sUsername = "";
            string sPassword = "";
            streamWriter.WriteLine("334 " + Base64Encode("Username:"));
            Log("--> 334 " + Base64Encode("Username:"));

            while (_socket.Connected == true)
            {
                if (streamReader == null) break;
                string line = streamReader.ReadLine();
                if (String.IsNullOrEmpty(line))
                    continue;
                Log("<-- " + line);

                if (iStage == 0)
                {
                    // This is the username
                    sUsername = Base64Decode(line);
                    streamWriter.WriteLine("334 " + Base64Encode("Password:"));
                    Log("--> " + "334 " + Base64Encode("Password:"));
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
                streamWriter.WriteLine("535 5.7.1 Authentication failed");
                Log("--> 535 5.7.1 Authentication failed");
                return false;
            }
            streamWriter.WriteLine("235 Authentication complete");
            Log("--> 235 2.7.0 Authentication complete");
            return true;
        }

        bool smtpAUTH(string AuthType, StreamReader streamReader, StreamWriter streamWriter)
        {
            // SMTP AUTH implementation

            if (_authed)
            {
                streamWriter.WriteLine("503 5.0.3 Already authenticated");
                Log("--> 503 5.0.3 Already authenticated");
                return false;
            }

            if (AuthType.StartsWith("LOGIN")) return smtpAUTHLOGIN(AuthType, streamReader, streamWriter);
            if (AuthType.StartsWith("PLAIN")) return smtpAUTHPLAIN(AuthType, streamReader, streamWriter);

            streamWriter.WriteLine("535 5.7.1 Authentication failed");
            Log("--> 535 5.7.1 Authentication failed");
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

        private void SaveMessage(string DATA, string MAILFROM)
        {
            if (String.IsNullOrEmpty(_storeFolder))
                return;

            string sFileName = MAILFROM + " " + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".eml";
            sFileName = _storeFolder + sFileName;
            Log("Saving message to: " + sFileName);
            try
            {
                StreamWriter oWriter = File.AppendText(sFileName);
                oWriter.Write(DATA);
                oWriter.Close();
            }
            catch (Exception ex)
            {
                Log("Error while saving message: " + ex.Message);
            }
        }

    }


}
