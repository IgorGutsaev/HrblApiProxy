using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DsCashLimitResult
    {
        [JsonPropertyName("LimitAmount")]
        public decimal LimitAmount { get; private set; }

        [JsonPropertyName("CashAmountPaid")]
        public decimal CashAmountPaid { get; private set; }

        [JsonPropertyName("Responses")]
        public Responses Responses { get; private set; }
    }
}