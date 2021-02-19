using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorDiscountResult
    {
        [JsonProperty("Distributor")]
        public DistributorDiscountDetails Discount { get; private set; }
    }

    public class DistributorDiscountDetails
    {
        [JsonProperty("DistributorType")]
        public string DistributorType { get; private set; }

        [JsonProperty("Discount")]
        public DistributorDiscount Discount { get; private set; }
    }
}