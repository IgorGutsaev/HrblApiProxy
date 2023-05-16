using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class ConversionRateResponse
    {
        [JsonPropertyName("ConversionRate")]
        public decimal? ConversionRate { get; set; }
    }
}