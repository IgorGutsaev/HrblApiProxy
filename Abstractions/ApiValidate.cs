using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class ApiValidateResponse
    {
        [JsonPropertyName("IsValid")]
        public bool IsValid { get; private set; }

        [JsonPropertyName("MemberId")]
        public string MemberId { get; private set; }
    }
}