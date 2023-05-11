using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SMTPTestSuite
{
    internal class SocketHandler
    {
        private Socket _socket;
        private Logger _logger;
        private SmtpMock _smtpMock;

        public SocketHandler(Socket socket, Logger logger, SmtpMock smtpMock)
        {
            _socket = socket;
            _logger = logger;
            _smtpMock = smtpMock;
        }

        public void ProcessSocket()
        {
            if (_smtpMock.ConnectionValidator.IsBlocked((System.Net.IPEndPoint)_socket.RemoteEndPoint, out string BlockReason))
            {
                _logger.Log($"Rejected connection from {((System.Net.IPEndPoint)_socket.RemoteEndPoint).Address}: {BlockReason}");
                try
                {
                    _socket.Close();
                    _socket.Dispose();
                }
                catch { }
                return;
            }

            _logger.Log($"Accepted connection from {((System.Net.IPEndPoint)_socket.RemoteEndPoint).Address}");

            SMTPSessionIncoming session = new SMTPSessionIncoming(_socket, _logger, !_smtpMock.OpenRelay,
                _smtpMock.MessageStoreFolder, _smtpMock.UseRecipientSubfolders);
            session.TLSCertificate = _smtpMock.TLSCertificate;
            session.RejectMessage = _smtpMock.RejectMessage;
            session.ConnectionValidator = _smtpMock.ConnectionValidator;
            
            session.Process();
        }
    }
}
