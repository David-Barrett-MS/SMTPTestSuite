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
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Logging;
using static System.Collections.Specialized.BitVector32;


namespace SMTPTestSuite
{
    /// <summary>
    /// Simple SMTP Mock server
    /// </summary>
    public class SmtpMock
    {
        private TcpListener _smtpListener = null;
        private Logger _logger = null;
        private System.Collections.ArrayList _sessions = new ArrayList();
        private bool _RequireAuth = true;
        private string _messageStoreFolder = "";
        private bool _useRecipientSubfolders = true;
        private string _rejectMessage = String.Empty; // If not empty, all messages are rejected with this message
        private int _rejectStage = 1; // The stage at which message is rejected (1 = RCPT TO)
        private X509Certificate _tlsCertificate = null;
        private List<string> _acceptedDomainList = new List<string>();
        private bool _listening = false;
        private Validators.Connection _connectionValidator = null;
        public static String ServerName { get; set; } = "mock.smtp.local";

        public SmtpMock(Logger logger)
        {
            _logger = logger;
            _connectionValidator = new Validators.Connection(logger);
        }

        public SmtpMock(Logger logger, string MessageStoreFolder): this(logger)
        {
            _messageStoreFolder = MessageStoreFolder;
        }

        ~SmtpMock()
        {
            this.Stop();
        }

        private void Log(string LogText, bool SectionBreakBefore=false)
        {
            try
            {
                _logger.Log(LogText, SectionBreakBefore);
            }
            catch { }
        }

        public bool OpenRelay
        {
            get { return _RequireAuth; }
            set { _RequireAuth = value; }
        }

        public X509Certificate TLSCertificate
        {
            get { return _tlsCertificate; }
            set { _tlsCertificate = value; }
        }

        public List<string> AcceptedDomainList
        {
            get { return _acceptedDomainList; }
            set { _acceptedDomainList = value; }
        }

        public string MessageStoreFolder
        {
            get { return _messageStoreFolder; }
            set
            {
                _messageStoreFolder = value;
                if (!_messageStoreFolder.EndsWith("\\"))
                    _messageStoreFolder += "\\";
            }
        }

        public bool UseRecipientSubfolders
        {
            get { return _useRecipientSubfolders; }
            set { _useRecipientSubfolders = value; }
        }

        public string RejectMessage
        {
            get { return _rejectMessage; }
            set { _rejectMessage = value; }
        }

        internal Validators.Connection ConnectionValidator
        {
            get { return _connectionValidator; }
        }


        /// <summary>
        /// list of all smtp sessions
        /// </summary>
        public SMTPSessionIncoming[] Sessions
        {
            get { return (SMTPSessionIncoming[])_sessions.ToArray(typeof(SMTPSessionIncoming)); }
        }


        /// <summary>
        /// start server
        /// </summary>
        public void Start()
        {
            Thread smtpServerThread = new Thread(new ThreadStart(Run));
            smtpServerThread.Start();
        }

        /// <summary>
        /// stop server
        /// </summary>
        public void Stop()
        {
            _listening = false;
            try
            {
                _smtpListener?.Stop();
            }
            catch { }
            _smtpListener = null;
            foreach (SMTPSessionIncoming session in _sessions)
            {
                try
                {
                    session.Quit();
                }
                catch { }
            }
        }




        /// <summary>
        /// run server
        /// </summary>
        private void Run()
        {
            _listening = true;
            try
            {
                _smtpListener = new TcpListener(IPAddress.Any, 25); // open listener for IP address 
                _smtpListener.Start();
            }
            catch
            {
                _smtpListener = null;
                return;
            }

            try
            {
                while (_listening)
                {
                    Socket clientSocket = null;
                    try
                    {
                        clientSocket = _smtpListener.AcceptSocket();
                    }
                    catch (Exception ex)
                    {
                        if (_listening)
                        {
                            Log("Exception when accepting connection: " + ex.Message);
                        }
                    }
                    if (!_listening)
                    {
                        try
                        {
                            clientSocket?.Close();
                        }
                        catch { }
                        clientSocket = null;
                    }
                    else if (clientSocket != null)
                    {
                        SocketHandler socketHandler = new SocketHandler(clientSocket, _logger, this);
                        Thread socketThread = new Thread(new ThreadStart(socketHandler.ProcessSocket));
                        socketThread.Start();
                    }
                }
            }
            catch { }
            try
            {
                _smtpListener?.Stop();
            }
            catch { }
        }
    }
}
