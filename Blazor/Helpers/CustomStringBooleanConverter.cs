using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ClassLibrary.Helpers
{
    /// <summary>
    /// Can deseerializr a boolean from a string
    /// </summary>
    public class CustomStringBooleanConverter : JsonConverter<bool>
    {
        public override bool Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) =>
            bool.Parse(reader.GetString());

        public override void Write(Utf8JsonWriter writer, bool value, JsonSerializerOptions options) =>
            writer.WriteStringValue(value ? "true" : "false");
    }
}
