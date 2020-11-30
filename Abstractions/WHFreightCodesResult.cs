using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class WHFreightCodesResult : WHFreightCodes
    {
        [JsonProperty("PostalCode")]
        public string PostalCode { get; private set; }

        [JsonProperty("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }

    public class WHFreightCodes
    {
        [JsonProperty("WHFriehtCodes")]
        public WHFreightCode[] WHFriehtCodes { get; private set; }

        [JsonProperty("ExpressDeliveryFlag")]
        public string _isExpressDeliveryFlag { get; private set; }

        [JsonIgnore]
        public bool ExpressDeliveryFlag => _isExpressDeliveryFlag.Equals("y", StringComparison.InvariantCultureIgnoreCase);
    }

    public class WHFreightCode
    {
        [JsonProperty("ShippingWareHouse")]
        public string ShippingWareHouse { get; private set; }

        [JsonProperty("FrieghtCode")]
        public string FrieghtCode { get; private set; }
    }
}
