using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using ClientConnector.messages;

namespace ClientConnector.data
{
    class HandshakeConverter : JsonConverter<CarrierPigeon<Handshake>>
    {
        public override CarrierPigeon<Handshake> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, CarrierPigeon<Handshake> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
