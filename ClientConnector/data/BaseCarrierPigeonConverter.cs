using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientConnector.messages;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace ClientConnector.data
{
    public class BaseCarrierPigeonConverter : JsonConverter<CarrierPigeon<object>>
    {
        public override CarrierPigeon<object> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, CarrierPigeon<object> value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
