using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Filuet.Hrbl.Ordering.Common;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Serializers
{

    public class PromotionRedemptionLimitJsonConverter : JsonConverter<PromotionRedemptionLimit>
    {
        public override PromotionRedemptionLimit Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelper.GetValueFromDescription<PromotionRedemptionLimit>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, PromotionRedemptionLimit redLimit, JsonSerializerOptions options)
            => writer.WriteStringValue(redLimit.GetDescription());
    }    
    
    public class PromotionRedemptionTypeJsonConverter : JsonConverter<PromotionRedemptionType>
    {
        public override PromotionRedemptionType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => EnumHelper.GetValueFromDescription<PromotionRedemptionType>(reader.GetString());

        public override void Write(Utf8JsonWriter writer, PromotionRedemptionType redType, JsonSerializerOptions options)
            => writer.WriteStringValue(redType.GetDescription());
    }
}
