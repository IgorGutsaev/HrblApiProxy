using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class ApiValidateResponse
    {
        [JsonPropertyName("IsValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("MemberId")]
        public string MemberId { get; set; }
    }
}