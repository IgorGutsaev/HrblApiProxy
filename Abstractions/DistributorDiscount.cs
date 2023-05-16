using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorDiscountResult
    {
        [JsonPropertyName("Distributor")]
        public DistributorDiscountDetails Discount { get; set; }
    }

    public class DistributorDiscountDetails
    {
        [JsonPropertyName("DistributorType")]
        public string DistributorType { get; set; }

        [JsonPropertyName("Discount")]
        public DistributorDiscount Discount { get; set; }
    }
}