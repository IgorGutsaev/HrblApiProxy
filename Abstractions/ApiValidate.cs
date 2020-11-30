using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class ApiValidateResponse
    {
        [JsonProperty("IsValid")]
        public bool IsValid { get; private set; }

        [JsonProperty("MemberId")]
        public string MemberId { get; private set; }
    }
}
