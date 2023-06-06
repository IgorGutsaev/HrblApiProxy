using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.ProxySDK.Models
{
    public class VPRequest
    {
        [Required]
        [JsonPropertyName("member")]
        public string MemberId { get; set; }

        [Required]
        [JsonPropertyName("month")]
        public DateTime Month { get; set; } = DateTime.UtcNow;

        [JsonPropertyName("monthTo")]
        public DateTime? MonthTo { get; set; }
    }
}