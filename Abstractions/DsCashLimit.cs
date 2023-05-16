using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DsCashLimitResult
    {
        [JsonPropertyName("LimitAmount")]
        public decimal LimitAmount { get; set; }

        [JsonPropertyName("CashAmountPaid")]
        public decimal CashAmountPaid { get; set; }

        [JsonPropertyName("Responses")]
        public Responses Responses { get; set; }
    }
}