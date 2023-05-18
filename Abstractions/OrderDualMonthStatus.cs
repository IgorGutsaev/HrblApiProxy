using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class OrderDualMonthStatus
    {
        [JsonPropertyName("ISDualMonthAllowed")]
        public string _dualMonth { get; set; }

        [JsonIgnore]
        public bool IsDualMonthAllowed => string.Equals(_dualMonth, "y", StringComparison.InvariantCultureIgnoreCase);
    }
}