using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class Responses
    {
        [JsonPropertyName("Response")]
        public Response[] Response { get; set; }
    }

    public class Response
    {
        [JsonPropertyName("ResponseCode")]
        public string Code { get; set; }

        [JsonPropertyName("ResponseMessage")]
        public string Message { get; set; }
    }
}