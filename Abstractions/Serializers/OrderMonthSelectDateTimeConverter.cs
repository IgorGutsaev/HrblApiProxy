using System.Globalization;
using System.Text.Json;
using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Serializers
{
    public class OrderMonthSelectDateTimeConverter : JsonConverter<DateTime>
    {
        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert,JsonSerializerOptions options) 
            => DateTime.ParseExact(reader.GetString()!, "yyMM", CultureInfo.InvariantCulture);

        public override void Write(Utf8JsonWriter writer, DateTime dateTimeValue, JsonSerializerOptions options)
            => writer.WriteStringValue(dateTimeValue.ToString("yyMM", CultureInfo.InvariantCulture));
    }
}