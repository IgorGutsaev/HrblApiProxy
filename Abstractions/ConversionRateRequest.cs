using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class ConversionRateRequest
    {
        [JsonProperty("FromCurrency")]
        public string FromCurrency { get; set; }

        [JsonProperty("ToCurrency")]
        public string ToCurrency { get; set; }

        [JsonProperty("ConversionDate")]
        public string ConversionDate { get; set; }

        [JsonProperty("ExchangeRateType")]
        public string ExchangeRateType { get; set; }

        public void SetDate(DateTime date)
        {
            ConversionDate = date.ToString("yyyy-MM-ddTHH:mm:ss.ffff");
        }
    }
}
