using Newtonsoft.Json;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DsCashLimitResult
    {
        [JsonProperty("LimitAmount")]
        public decimal LimitAmount { get; private set; }

        [JsonProperty("CashAmountPaid")]
        public decimal CashAmountPaid { get; private set; }

        [JsonProperty("Responses")]
        public Responses Responses { get; private set; }
    }
}
