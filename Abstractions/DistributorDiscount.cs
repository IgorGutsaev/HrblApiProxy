using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorDiscountResult
    {
        [JsonPropertyName("Distributor")]
        public DistributorDiscountDetails Discount { get; private set; }
    }

    public class DistributorDiscountDetails
    {
        [JsonPropertyName("DistributorType")]
        public string DistributorType { get; private set; }

        [JsonPropertyName("Discount")]
        public DistributorDiscount Discount { get; private set; }
    }
}