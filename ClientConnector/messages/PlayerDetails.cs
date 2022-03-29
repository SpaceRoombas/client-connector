using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.data
{
    /**
     * 
     * The intent of this object is that its contents will come from the match making server
     * The idea is that this will get passed on handshake. The client will hold onto this token
     * And pass it to the session server to be let into the session.
     * */
    public class PlayerDetails
    {
        public string PlayerName;
        public string ServerAddress;
        public int TokenTimeMillis;
        public int MatchEndTimeMillis;
        // This will most likely be the resulting HMAC hash of 
        // PlayerName+ServerAddress+TokenTimeMillis+MatchEndTimeMillis 
        public string HMACString;
    }
}
