using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class Responses
    {
        [JsonPropertyName("Response")]
        public Response[] Response { get; private set; }
    }

    public class Response
    {
        [JsonPropertyName("ResponseCode")]
        public string Code { get; private set; }

        [JsonPropertyName("ResponseMessage")]
        public string Message { get; private set; }
    }
}