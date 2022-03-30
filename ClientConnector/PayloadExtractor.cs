
using ClientConnector.messages;
using System;

namespace ClientConnector
{
    public class PayloadExtractor
    {

        static Func<ICarrierPigeon, string, bool> Verify = (carrier, payloadType) => carrier.GetPayloadType() == payloadType;
        public static MapSector GetMapSector(ICarrierPigeon carrier)
        {
            if(Verify(carrier, "MapSector"))
            {
                return ((CarrierPigeon<MapSector>)carrier).payload;
            }

            return null;
        }

        public static Handshake GetHandshake(ICarrierPigeon carrier)
        {
            if (Verify(carrier, "Handshake"))
            {
                return ((CarrierPigeon<Handshake>)carrier).payload;
            }

            return null;
        }


    }
}
