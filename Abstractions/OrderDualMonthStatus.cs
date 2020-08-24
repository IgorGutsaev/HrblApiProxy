using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class OrderDualMonthStatus
    {
        [JsonProperty("ISDualMonthAllowed")]
        private string _dualMonth { get; set; }

        [JsonIgnore]
        public bool IsDualMonthAllowed => string.Equals(_dualMonth, "y", StringComparison.InvariantCultureIgnoreCase);
    }
}
