using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.messages
{
    class Handshake
    {
        public const int STATUS_OK = 200;
        public const int STATUS_ORPHAN_OK = 201;
        public const int STATUS_VERIFY_FAILED = 403;

        public string username;
        public string signature;
        public int status;

        public Handshake()
        {
        }

        public Handshake(string username, string signature, int status)
        {
            this.username = username;
            this.signature = signature;
            this.status = status;
        }


    }
}
