using ClientConnector.messages;
using ClientConnector.data;
using System.Text.Json;
using System.Collections.Generic;
using System;

namespace ClientConnector
{
    class MessageSerializer
    {

        private JsonSerializerOptions serializerOptions;
        private Dictionary<string, Type> deserializationTypes;
        public MessageSerializer()
        {
            this.serializerOptions = new JsonSerializerOptions();
            this.serializerOptions.Converters.Add(new SerializationConverterFactory());
        }

        private void intializeDeserializationTypes()
        {
            this.deserializationTypes = new Dictionary<string, Type>();
            this.deserializationTypes.Add("handshake", typeof(Handshake));
        }
        public string SerializeMessage(CarrierPigeon<PlayerDetails> message)
        {
            string serialized = JsonSerializer.Serialize(message, message.GetType(), this.serializerOptions);
            return serialized;
        }

        public CarrierPigeon<Object> DeserializeMessage(string raw)
        {

            // TODO Properly deserialize incoming messages
            CarrierPigeon<Object> carrier = (CarrierPigeon<Object>) JsonSerializer.Deserialize(raw, typeof(CarrierPigeon<Object>));
            //JsonSerializer.Deserialize(raw);
            return null;
        }
    }
}
