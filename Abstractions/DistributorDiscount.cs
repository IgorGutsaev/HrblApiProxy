using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class DistributorDiscountResult
    {
        [JsonProperty("Distributor")]
        public DistributorDiscount Discount { get; private set; }
    }
}
