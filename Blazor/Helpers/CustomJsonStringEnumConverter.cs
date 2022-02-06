using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ClassLibrary.Helpers
{
    /// <summary>
    /// Can convert Enumerals from a name or value
    /// </summary>
    public class CustomJsonStringEnumConverter : JsonConverterFactory
    {
        private readonly JsonStringEnumConverter baseConverter;

        public CustomJsonStringEnumConverter(JsonNamingPolicy namingPolicy = null, bool allowIntegerValues = true)
        {
            baseConverter = new JsonStringEnumConverter(namingPolicy, allowIntegerValues);
        }

        public override bool CanConvert(Type typeToConvert) => baseConverter.CanConvert(typeToConvert);

        public override JsonConverter CreateConverter(Type typeToConvert, JsonSerializerOptions options)
        {
            return baseConverter.CreateConverter(typeToConvert, options);
        }
    }

}
