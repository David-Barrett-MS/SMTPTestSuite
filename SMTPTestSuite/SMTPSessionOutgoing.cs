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
using System.Linq;
using System.Net.Sockets;
using System.Net.Security;
using System.Threading;
using System.Security.Cryptography.X509Certificates;

namespace SMTPTestSuite
{
    class SMTPSessionOutgoing
    {
        private string _server;
        private int _port=25;
        private int _timeout = 15;
        private bool _useStartTLS = false;
        private bool _ignoreCertificateErrors = false;
        Logging.Logger _Logger = null;

        public SMTPSessionOutgoing(string Server, int Port)
        {
            _server = Server;
            _port = Port;
        }

        public SMTPSessionOutgoing(string Server, int Port, Logging.Logger Logger, bool useSSL, bool IgnoreSSLCertificateErrors)
            : this(Server, Port)
        {
            _Logger = Logger;
            _useStartTLS = useSSL;
            _ignoreCertificateErrors = IgnoreSSLCertificateErrors;
        }

        private void Log(string LogItem)
        {
            if (_Logger == null) return;

            string prefix = "";
            if (LogItem.StartsWith("<--"))
                prefix = "<--";
            else if (LogItem.StartsWith("-->"))
                prefix = "-->";
            
            if (LogItem.Contains(Environment.NewLine) && prefix != "")
            {
                if (LogItem.StartsWith("<--"))
                    prefix = "<--";
                else if (LogItem.StartsWith("-->"))
                    prefix = "-->";

                string[] lines = LogItem.Substring(3).Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);
                for (int i=0; i<lines.Length; i++)
                {
                    if (i < lines.Length-1)
                        _Logger.Log($"{prefix}{lines[i]}");
                    else if (!String.IsNullOrEmpty(lines[i]))
                        _Logger.Log($"{prefix}{lines[i]}");
                }
            }
            else
                _Logger.Log(LogItem);
        }

        private string ValidateEnvelopeRecipient(string envelopeRecipient)
        {
            if (!envelopeRecipient.Contains('@'))
                return envelopeRecipient;

            if (!envelopeRecipient.StartsWith("<")) envelopeRecipient = "<" + envelopeRecipient;
            if (!envelopeRecipient.EndsWith("<")) envelopeRecipient = envelopeRecipient + ">";

            return envelopeRecipient;
        }

        private string ReadSSLStream(SslStream Stream)
        {
            byte[] buffer = new byte[2048];
            int bytes = Stream.Read(buffer, 0, buffer.Length);
            string response = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
            Log("<-- " + response);
            return response;
        }

        private void WriteSSLStream(SslStream Stream, string Data)
        {
            Stream.Write(System.Text.Encoding.ASCII.GetBytes(Data));
            Stream.Flush();
            Log("--> " + Data);
        }

        private string ReadNetworkStream(NetworkStream Stream)
        {           
            DateTime EndTime = DateTime.Now.AddSeconds(_timeout);
            while ((!Stream.DataAvailable) && (DateTime.Now < EndTime))
            {
                Thread.Sleep(100);
            }

            if (!Stream.DataAvailable)
            {
                Log("Time out waiting for response");
                return null;
            }

            byte[] buffer = new byte[2048];
            int bytes = Stream.Read(buffer, 0, buffer.Length);
            string response = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
            Log("<-- " + response);
            return response;
        }

        private void WriteNetworkStream(NetworkStream Stream, string Data)
        {
            byte[] dataBytes = System.Text.Encoding.ASCII.GetBytes(Data);
            Stream.Write(dataBytes, 0, dataBytes.Length);
            Stream.Flush();
            Log("--> " + Data);
        }

        public bool ValidateServerCertificate(
              object sender,
              X509Certificate certificate,
              X509Chain chain,
              SslPolicyErrors sslPolicyErrors)
        {
            if (sslPolicyErrors == SslPolicyErrors.None)
                return true;

            // Do not allow this client to communicate with unauthenticated servers.
            Log($"Certificate error: {sslPolicyErrors}");
            return _ignoreCertificateErrors;
        }


        /// <summary>
        /// SendMessage
        /// </summary>
        public string SendMessage(string MAILFROM, string RCPTTO, string DATA)
        {
            Log("Connecting to " + _server + ":" + _port);
            string successMessage = "";

            try
            {
                using (TcpClient tcpClient = new TcpClient(_server, _port))
                {
                    NetworkStream oStream = tcpClient.GetStream();

                    while (!oStream.DataAvailable)
                        Thread.Sleep(50);
                    Byte[] data = new Byte[1024];
                    String sData = String.Empty;
                    while (oStream.DataAvailable)
                    {
                        Int32 bytes = oStream.Read(data, 0, data.Length);
                        sData += System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    }
                    Log("<-- " + sData);

                    string sResponse = SendData(oStream, $"EHLO");// {System.Windows.Forms.Application.ProductName} v{System.Windows.Forms.Application.ProductVersion}");
                    if (!sResponse.StartsWith("250"))
                        throw new Exception($"Unexpected EHLO response: {sResponse}");


                    if (_useStartTLS)
                    {
                        // Encrypted connection required

                        sResponse = SendData(oStream, "STARTTLS");
                        if (!sResponse.StartsWith("220"))
                            throw new Exception($"Failed to initialise TLS: {sResponse}");

                        using (SslStream sslStream = new SslStream(oStream,false, ValidateServerCertificate))
                        {
                            // Now we can initialise and communicate over an encrypted connection
                            sslStream.AuthenticateAsClient(_server);

                            // EHLO again
                            sResponse = SendData(sslStream, "EHLO");
                            if (!sResponse.StartsWith("250"))
                                throw new Exception($"Unexpected EHLO response: {sResponse}");

                            // MAIL FROM
                            sResponse = SendData(sslStream, "MAIL FROM:" + ValidateEnvelopeRecipient(MAILFROM));
                            if (!sResponse.StartsWith("250"))
                                throw new Exception($"Failed at MAIL FROM: {sResponse}");

                            // RCPT TO
                            sResponse = SendData(sslStream, "RCPT TO:" + ValidateEnvelopeRecipient(RCPTTO));
                            if (!sResponse.StartsWith("250"))
                                throw new Exception($"Failed at RCPT TO: {sResponse}");

                            // DATA
                            sResponse = SendData(sslStream, "DATA");
                            if (!sResponse.StartsWith("354"))
                                throw new Exception($"Failed at DATA: {sResponse}");
                            successMessage = SendData(sslStream, DATA + "\r\n.");

                            SendData(sslStream, "QUIT", false);
                        }
                    }
                    else
                    {
                        // No encryption
                        // MAIL FROM
                        sResponse = SendData(oStream, "MAIL FROM:" + ValidateEnvelopeRecipient(MAILFROM));
                        if (!sResponse.StartsWith("250"))
                            throw new Exception($"Failed at MAIL FROM: {sResponse}");

                        // RCPT TO
                        bool bRecipValid = false;
                        if (RCPTTO.Contains(";"))
                        {
                            // Multiple recipients
                            string[] sRecips = RCPTTO.Split(';');
                            foreach (string sRecip in sRecips)
                            {
                                sResponse = SendData(oStream, "RCPT TO:" + ValidateEnvelopeRecipient(sRecip));
                                if (!bRecipValid)
                                    bRecipValid = (sResponse.StartsWith("250"));
                            }
                        }
                        else
                        {
                            // Single recipient
                            sResponse = SendData(oStream, "RCPT TO:" + ValidateEnvelopeRecipient(RCPTTO));
                            bRecipValid = (sResponse.StartsWith("250"));
                        }
                        if (!bRecipValid)
                            throw new Exception($"Failed at RCPT TO: {sResponse}");

                        // DATA
                        sResponse = SendData(oStream, "DATA");
                        if (!sResponse.StartsWith("354"))
                            throw new Exception($"Failed at DATA: {sResponse}");
                        successMessage = SendData(oStream, DATA + "\r\n.");

                        SendData(oStream, "QUIT", false);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Sending message failed: " + ex.Message);
                return null;
            }

            return successMessage;
        }

        private string SendData(NetworkStream Stream, string Data, bool ReadResponse=true)
        {
            WriteNetworkStream(Stream, $"{Data}\r\n");
            if (!ReadResponse)
                return null;

            return ReadNetworkStream(Stream);
        }

        private string SendData(SslStream Stream, string Data, bool ReadResponse = true)
        {
            WriteSSLStream(Stream, $"{Data}\r\n");
            
            if (!ReadResponse)
                return null;

            return ReadSSLStream(Stream);
        }
    }
}

