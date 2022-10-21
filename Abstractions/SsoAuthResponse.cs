using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SsoAuthResposeDto
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }

        [JsonPropertyName("refreshToken")]
        public string RefreshToken { get; set; }

        public bool IsAuthenticated { get; set; }
    }

    public class SsoAuthResposeWrapper
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("data")]
        public SsoAuthResposeDto Data { get; set; }
    }
}