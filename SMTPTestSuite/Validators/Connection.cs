using Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;


namespace SMTPTestSuite.Validators
{
    internal class Connection
    {
        private Dictionary<IPAddress, int> _badBehaviourTally;
        private List<IPAddress> _blockedEndpoints;
        private Dictionary<IPAddress, DateTime> _spamhausBlockList;
        private readonly object _lockObject = new object();
        private Logger _logger;
        private bool _invalidSpamhausDNS = false;
        private bool _checkSpamhaus = true;
        private bool _blockSpamhaus = false;

        public Connection(Logger logger)
        {
            _logger = logger;
            InitBlockLists();
        }

        public Connection(Logger logger, bool CheckSpamhaus, bool BlockSpamhaus):this(logger)
        {
            _checkSpamhaus = CheckSpamhaus;
            _blockSpamhaus = BlockSpamhaus;
        }

        private void InitBlockLists()
        {
            _badBehaviourTally = new Dictionary<IPAddress, int>();
            _blockedEndpoints = new List<IPAddress>();
            _spamhausBlockList = new Dictionary<IPAddress, DateTime>();
        }

        public bool CheckSpamhaus
        {
            get { return _checkSpamhaus; }
            set { _checkSpamhaus = value; }
        }

        public bool BlockSpamhaus
        {
            get { return _blockSpamhaus; }
            set { _blockSpamhaus = value; }
        }

        public void LogBadBehaviour(IPEndPoint badClient)
        {
            lock (_lockObject)
                if (_blockedEndpoints.Contains(badClient.Address))
                    return;

            bool clientBlocked = false;
            if (_badBehaviourTally.ContainsKey(badClient.Address))
            {
                lock (_lockObject)
                {
                    _badBehaviourTally[badClient.Address] = _badBehaviourTally[badClient.Address] + 1;
                    if (_badBehaviourTally[badClient.Address] > 3)
                    {
                        _blockedEndpoints.Add(badClient.Address);
                        _badBehaviourTally.Remove(badClient.Address);
                        clientBlocked = true;
                    }
                    else
                        _logger.Log($"{badClient.Address} noted for bad behaviour ({_badBehaviourTally[badClient.Address]} infractions)");
                }
                if (clientBlocked)
                    _logger.Log($"Added {badClient.Address} to block list");
                return;
            }

            lock (_lockObject)
                _badBehaviourTally.Add(badClient.Address, 1);
            _logger.Log($"{badClient.Address} noted for bad behaviour (first infraction). {_badBehaviourTally.Count} client(s) known.");
        }

        public bool IsBlocked(IPEndPoint client, out string BlockReason)
        {
            BlockReason = "";
            lock (_lockObject)
                if (_blockedEndpoints.Contains(client.Address))
                {
                    BlockReason = "Listed in local blocklist";
                    return true;
                }

            if (!_checkSpamhaus)
                return false;

            bool spamhausBlock = BlockedBySpamhaus(client);
            if (spamhausBlock && _blockSpamhaus)
            {
                BlockReason = "Listed in Spamhaus blocklist";
                return true;
            }

            return false;
        }

        private bool BlockBasedOnSpamhausResponse(byte b1, byte b2, byte b3, byte b4)
        {
            if (b1 != 127)
                return false;
            if (b2 == 255 && b3 == 255)
            {
                if (b4 == 254)
                {
                    _logger.Log("Spamhaus disabled due to public DNS server configuration");
                    _invalidSpamhausDNS = true;
                }
                return false;
            }

            if (b2==0 && b3==0)
                if ( b4==2 || b4 == 3 || b4 == 4 || b4 == 9 || b4 == 10 || b4 == 11)
                    return true;
            return false;
        }

        private bool BlockBasedOnSpamhausResponse(byte[] spamHausResponse)
        {
            return BlockBasedOnSpamhausResponse(spamHausResponse[0], spamHausResponse[1], spamHausResponse[2], spamHausResponse[3]);
        }

        public bool BlockedBySpamhaus(IPEndPoint client)
        {
            if (_invalidSpamhausDNS)
                return false;

            if (_spamhausBlockList.ContainsKey(client.Address))
            {
                // We've already checked this client, we'll only recheck once every 24 hours
                if (DateTime.Now.Subtract(_spamhausBlockList[client.Address]).TotalHours < 24)
                {
                    _logger.Log($"{client.Address} found in Spamhaus cache");
                    return true;
                }
                lock (_lockObject)
                    _spamhausBlockList.Remove(client.Address);
            }

            try
            {
                byte[] addressBytes = client.Address.GetAddressBytes();
                string spamhausQuery = $"{addressBytes[3]}.{addressBytes[2]}.{addressBytes[1]}.{addressBytes[0]}.zen.spamhaus.org";

                IPHostEntry hostEntry = Dns.GetHostEntry(spamhausQuery);
                byte[] responseBytes = hostEntry.AddressList[0].GetAddressBytes();
                _logger.Log($"{spamhausQuery} returned {responseBytes[0]}.{responseBytes[1]}.{responseBytes[2]}.{responseBytes[3]}");
                if (BlockBasedOnSpamhausResponse(responseBytes))
                {
                    _spamhausBlockList.Add(client.Address, DateTime.Now);
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }
    }
}
