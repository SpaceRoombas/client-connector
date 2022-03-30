using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class Handshake
    {
        public const int STATUS_OK = 200;
        public const int STATUS_ORPHAN_OK = 201;
        public const int STATUS_VERIFY_FAILED = 403;

        [JsonPropertyName("username")]
        public string username { get; set; }
        [JsonPropertyName("signature")]
        public string signature { get; set; }
        [JsonPropertyName("status")]
        public int status { get; set; }

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
