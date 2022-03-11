using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class ConversionRateResponse
    {
        [JsonProperty("ConversionRate")]
        public decimal? ConversionRate { get; set; }
    }
}
