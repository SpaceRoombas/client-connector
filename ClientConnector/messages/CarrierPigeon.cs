using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace ClientConnector.messages
{
    public class CarrierPigeon<T> : ICarrierPigeon
    {
        [JsonPropertyName("payload")]
        public T payload { get; set; }
        [JsonPropertyName("payload_type")]
        public string payloadType { get; set; }
        [JsonPropertyName("type")]
        public string messageType { get; set; }

        public CarrierPigeon(T payload, string payloadType, string messageType)
        {
            this.payload = payload;
            this.payloadType = payloadType;
            this.messageType = messageType;
        }

        public string GetPayloadType()
        {
            return this.payloadType;
        }

        public string GetMessageType()
        {
            return this.messageType;
        }

        public void SetPayloadObject(object payload)
        {
            // TODO this could cause problems, double check that empty payloads are handled in backend
            if(payload.GetType() != typeof(T))
            {
                this.payload = default(T);
                return;
            }
            this.payload = (T) payload;
        }
    }
}
