using Filuet.Hrbl.Ordering.Abstractions.Enums;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Models
{
    public class PollResult
    {
        [JsonPropertyName("level")]
        public ActionLevel Level { get; set; }        
        
        [JsonPropertyName("date")]
        public DateTimeOffset Timestamp { get; set; }

        [JsonPropertyName("items")]
        public IEnumerable<PollUnitResult> Items { get; set; }
    }

    public class PollUnitResult
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }

        [JsonPropertyName("level")]
        public ActionLevel Level { get; set; }

        [JsonPropertyName("comment")]
        public string Comment { get; set; }

        public override string ToString() => Action;
    }
}