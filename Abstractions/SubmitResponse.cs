using Newtonsoft.Json;
using System;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SubmitResponse
    {
        [JsonProperty("OrderStatus")]
        public string OrderStatus { get; set; } //  SUCCESS
        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; set; }
        [JsonIgnore]
        public bool IsSuccess => string.Equals(OrderStatus, "SUCCESS", StringComparison.InvariantCultureIgnoreCase);
    }
}
