using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class WHFreightCodesResult : WHFreightCodes
    {
        [JsonPropertyName("PostalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }

    public class WHFreightCodes
    {
        [JsonPropertyName("WHFriehtCodes")]
        public WHFreightCode[] WHFriehtCodes { get; set; }

        [JsonPropertyName("ExpressDeliveryFlag")]
        public string _isExpressDeliveryFlag { get; set; }

        [JsonIgnore]
        public bool ExpressDeliveryFlag => _isExpressDeliveryFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);
    }

    public class WHFreightCode
    {
        [JsonPropertyName("ShippingWareHouse")]
        public string ShippingWareHouse { get; set; }

        [JsonPropertyName("FrieghtCode")]
        public string FrieghtCode { get; set; }
    }
}