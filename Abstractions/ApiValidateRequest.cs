using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class ApiValidateRequest
    {
        [JsonPropertyName("X-HLAPPID")]
        public string AppId { get; private set; }
        [JsonPropertyName("AccessToken")]
        public string AccessToken { get; private set; }

        internal static ApiValidateRequest Create(uint organizationId, string token)
        {
            if (organizationId == 0)
                throw new ArgumentException("Organization Id is mandatory");

            if (string.IsNullOrWhiteSpace(token))
                throw new ArgumentException("Token is empty!");

            ApiValidateRequest result = new ApiValidateRequest();
            result.AppId = organizationId.ToString();
            result.AccessToken = token.Trim().ToString();

            return result;
        }
    }
}