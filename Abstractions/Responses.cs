using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class Responses
    {
        [JsonProperty("Response")]
        public Response[] Response { get; private set; }
    }

    public class Response
    {
        [JsonProperty("ResponseCode")]
        public string Code { get; private set; }

        [JsonProperty("ResponseMessage")]
        public string Message { get; private set; }
    }
}
