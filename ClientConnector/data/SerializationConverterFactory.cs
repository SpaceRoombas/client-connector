using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Collections.Generic;
using ClientConnector.messages;


namespace ClientConnector.data
{
    class SerializationConverterFactory : JsonConverterFactory
    {
        private Dictionary<string, Func<JsonConverter>> converters;
        
        public SerializationConverterFactory()
        {
            InitializeConverters();
        }

        private void InitializeConverters()
        {
            this.converters = new Dictionary<string, Func<JsonConverter>>();
            this.converters.Add(typeof(Handshake).Name, () => new HandshakeConverter());
            this.converters.Add(typeof(PlayerDetails).Name, () => new PlayerDetailsConverter());
            this.converters.Add(typeof(CarrierPigeon<Object>).Name, () => new BaseCarrierPigeonConverter());
        }

        public override bool CanConvert(Type type)
        {
            if(!type.IsGenericType)
            {
                return false;
            }

            string typeName = type.GenericTypeArguments[0].Name;
            return this.converters.ContainsKey(typeName);
        }

        public override JsonConverter CreateConverter(Type type, JsonSerializerOptions options)
        {
            string typeName = type.GenericTypeArguments[0].Name;
            var converterCreator = this.converters[typeName];
            JsonConverter converter = converterCreator();
            return converter;
        }
    }
}
