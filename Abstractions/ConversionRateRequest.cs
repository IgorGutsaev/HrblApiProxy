using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class ConversionRateRequest
    {
        [JsonPropertyName("FromCurrency")]
        public string FromCurrency { get; set; }

        [JsonPropertyName("ToCurrency")]
        public string ToCurrency { get; set; }

        [JsonPropertyName("ConversionDate")]
        public string ConversionDate { get; set; }

        [JsonPropertyName("ExchangeRateType")]
        public string ExchangeRateType { get; set; }

        public void SetDate(DateTime date)
        {
            ConversionDate = date.ToString("yyyy-MM-ddTHH:mm:ss.ffff");
        }
    }
}