using ClientConnector.messages;
using System.Text.Json;
using System.Collections.Generic;
using System;

namespace ClientConnector
{
    class MessageSerializer
    {

        //private JsonSerializerOptions serializerOptions;
        private Dictionary<string, Type> deserializationTypes;
        private Dictionary<string, Func<ICarrierPigeon>> carrierTypes;
        public MessageSerializer()
        {
            intializeDeserializationTypes();
            initalizeCarrierTypes();
        }

        private void intializeDeserializationTypes()
        {
            this.deserializationTypes = new Dictionary<string, Type>();
            this.deserializationTypes.Add("Handshake", typeof(CarrierPigeon<Handshake>));
            this.deserializationTypes.Add("MapSector", typeof(CarrierPigeon<MapSector>));
        }

        private void initalizeCarrierTypes()
        {
            this.carrierTypes = new Dictionary<string, Func<ICarrierPigeon>>();
            this.carrierTypes.Add("Handshake", () => new CarrierPigeon<Handshake>(null, "Handshake", "handshake"));
            this.carrierTypes.Add("PlayerDetails", () => new CarrierPigeon<PlayerDetails>(null, "player_details", "handshake"));
            this.carrierTypes.Add("PlayerFirmwareChange", () => new CarrierPigeon<PlayerFirmwareChange>(null, "firmware_change", "message"));
        }

        private Type resolvePayloadType(string json)
        {
            JsonDocument doc;
            string payloadTypeString;
            Type payloadType;

            doc = JsonDocument.Parse(json);

            try
            {
                payloadTypeString = doc.RootElement.GetProperty("payload_type").GetString();
                payloadType = this.deserializationTypes[payloadTypeString];

            } catch(KeyNotFoundException e)
            {
                Console.WriteLine("Recieved bad carrier payload type");
                return null; // TODO Do better
            }

            return payloadType;
        }
        public string SerializeMessage(ICarrierPigeon message)
        {
            return JsonSerializer.Serialize(message, message.GetType());
        }

        public ICarrierPigeon DeserializeMessage(string raw)
        {
            Type payloadType = resolvePayloadType(raw);

            if(payloadType == null)
            {
                return Util.generateInternalErrorMessage("Invalid Carrier Payload");
            }
            return (ICarrierPigeon) JsonSerializer.Deserialize(raw, payloadType);
        }

        public string SerializeObject(object message)
        {
            string messageType = message.GetType().Name;
            var carrierCreator = carrierTypes[messageType];
            ICarrierPigeon carrier = carrierCreator();
            carrier.SetPayloadObject(message);
            // TODO DEFINITELY TEST THIS
            return this.SerializeMessage(carrier);
        }
    }
}
