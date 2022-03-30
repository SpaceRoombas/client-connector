using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    /**
     * 
     * The intent of this object is that its contents will come from the match making server
     * The idea is that this will get passed on handshake. The client will hold onto this token
     * And pass it to the session server to be let into the session.
     * */
    public class PlayerDetails
    {
        [JsonPropertyName("player_name")]
        public string PlayerName { get; set; }
        [JsonPropertyName("server_address")]
        public string ServerAddress { get; set; }
        [JsonPropertyName("token_millis")]
        public int TokenTimeMillis { get; set; }
        [JsonPropertyName("match_end_millis")]
        public int MatchEndTimeMillis { get; set; }
        // This will most likely be the resulting HMAC hash of 
        // PlayerName+ServerAddress+TokenTimeMillis+MatchEndTimeMillis 
        [JsonPropertyName("signature")]
        public string HMACString { get; set; }
    }
}
