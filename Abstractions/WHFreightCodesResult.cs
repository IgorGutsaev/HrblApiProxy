using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class WHFreightCodesResult : WHFreightCodes
    {
        [JsonPropertyName("PostalCode")]
        public string PostalCode { get; private set; }

        [JsonPropertyName("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }

    public class WHFreightCodes
    {
        [JsonPropertyName("WHFriehtCodes")]
        public WHFreightCode[] WHFriehtCodes { get; private set; }

        [JsonPropertyName("ExpressDeliveryFlag")]
        public string _isExpressDeliveryFlag { get; private set; }

        [JsonIgnore]
        public bool ExpressDeliveryFlag => _isExpressDeliveryFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);
    }

    public class WHFreightCode
    {
        [JsonPropertyName("ShippingWareHouse")]
        public string ShippingWareHouse { get; private set; }

        [JsonPropertyName("FrieghtCode")]
        public string FrieghtCode { get; private set; }
    }
}