using Filuet.Hrbl.Ordering.Abstractions.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    public class PollResult
    {
        [JsonPropertyName("level")]
        [JsonProperty("level")]
        public ActionLevel Level { get; set; }        
        
        [JsonPropertyName("date")]
        [JsonProperty("date")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("items")]
        [JsonProperty("items")]
        public IEnumerable<PollUnitResult> Items { get; set; }
    }

    public class PollUnitResult
    {
        [JsonPropertyName("action")]
        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonPropertyName("level")]
        [JsonProperty("level")]
        public ActionLevel Level { get; set; }

        [JsonPropertyName("comment")]
        [JsonProperty("comment")]
        public string Comment { get; set; }

        public override string ToString() => Action;
    }
}
