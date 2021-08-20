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
using System.Collections;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Logging;


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
        private string _rejectMessage = String.Empty; // If not empty, all messages are rejected with this message
        private int _rejectStage = 1; // The stage at which message is rejected (1 = RCPT TO)

        public SmtpMock(Logger logger)
        {
            _logger = logger;
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

        public string RejectMessage
        {
            get { return _rejectMessage; }
            set { _rejectMessage = value; }
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
            try
            {
                if (_smtpListener != null)
                {                    
                    _smtpListener.Stop();
                }
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
            bool bListen = true;
            _smtpListener = new TcpListener(IPAddress.Any, 25); // open listener for IP address 
            _smtpListener.Start();

            try
            {
                while (bListen)
                {
                    Socket clientSocket = null;
                    try
                    {
                        clientSocket = _smtpListener.AcceptSocket();
                    }
                    catch (Exception ex)
                    {
                        Log("Exception when accepting connection: " + ex.Message);
                        bListen = false;
                    }
                    if (!bListen)
                    {
                        try
                        {
                            clientSocket?.Close();
                        }
                        catch { }
                        clientSocket = null;
                    }
                    else
                    {
                        SMTPSessionIncoming session = new SMTPSessionIncoming(clientSocket, _logger, !OpenRelay, _messageStoreFolder);
                        session.RejectMessage = _rejectMessage;
                        _sessions.Add(session);
                        Thread sessionThread = new Thread(new ThreadStart(session.Process));
                        sessionThread.Start();
                    }
                    /*sessionThread.Join (); // remove comment  for more sessions at the same time*/
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
