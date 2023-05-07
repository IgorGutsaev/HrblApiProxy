using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Proxy.Models
{
    public class AuthCredentials
    {
        [Required]
        [JsonPropertyName("login")]
        public string Login { get; set; }

        [Required]
        [JsonPropertyName("password")]
        public string Password { get; set; }
    }
}
