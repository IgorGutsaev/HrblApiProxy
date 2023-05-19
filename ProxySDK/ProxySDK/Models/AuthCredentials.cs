using Filuet.Infrastructure.Abstractions.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.ProxySDK.Models
{
    public class AuthCredentials
    {
        [Required]
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("force")]
        public bool Force { get; set; } = false;

        [JsonPropertyName("country")]
        public Country? Country { get; set; } = null;
    }
}
