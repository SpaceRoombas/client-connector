using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConnector.messages
{
    public class CarrierPigeon<T>
    {
        public T payload;
        public string payloadType;
        public string messageType;

        public CarrierPigeon(T payload, string payloadType, string messageType)
        {
            this.payload = payload;
            this.payloadType = payloadType;
            this.messageType = messageType;
        }

        public static explicit operator CarrierPigeon<T>(CarrierPigeon<object> v)
        {
            throw new NotImplementedException();
        }
    }
}
