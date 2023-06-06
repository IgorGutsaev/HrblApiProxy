using Filuet.Infrastructure.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.ProxySDK.Models
{
    public class MemberCountryRequest
    {
        [Required]
        [JsonPropertyName("member")]
        public string MemberId { get; set; }

        [Required]
        [JsonPropertyName("country")]
        public Country Country { get; set; }
    }
}