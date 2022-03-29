using System;
using System.Text.Json.Serialization;
using ClientConnector.messages;
using System.Text.Json;

namespace ClientConnector.data
{
    class PlayerDetailsConverter : JsonConverter<CarrierPigeon<PlayerDetails>>
    {
        public override CarrierPigeon<PlayerDetails> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, CarrierPigeon<PlayerDetails> value, JsonSerializerOptions options)
        {

            writer.WriteStartObject();
            
            writer.WriteString("type", value.messageType);
            writer.WriteString("payload_type", value.payloadType);

            // payload        
            writer.WritePropertyName("payload");
            writer.WriteStartObject();
            writer.WriteString("player_name", value.payload.PlayerName);
            writer.WriteString("server_address", value.payload.ServerAddress);
            writer.WriteNumber("token_millis", value.payload.TokenTimeMillis);
            writer.WriteNumber("match_end_millis", value.payload.MatchEndTimeMillis);
            writer.WriteString("signature", value.payload.HMACString);
            writer.WriteEndObject();

            // end of object
            writer.WriteEndObject();
        }
    }
}
