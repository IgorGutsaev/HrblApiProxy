using System.Globalization;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Serializers
{
    public class StandardDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => DateTime.Parse(reader.GetString()!);

        public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
            => writer.WriteStringValue(dateTimeValue.ToString("s", CultureInfo.InvariantCulture));
    }
}