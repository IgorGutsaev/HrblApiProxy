using Newtonsoft.Json;
using System.Linq;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class CommonErrorList
    {
        [JsonProperty("Error")]
        public CommonError[] Error { get; private set; } = new CommonError[0];

        [JsonIgnore]
        public bool HasErrors => Error.Any(x => x.ErrorCode != "0");

        [JsonIgnore]
        public string ErrorMessage => !HasErrors ? string.Empty :
            string.Join("\n",Error.Where(x => x.ErrorCode != "0").Select(x => x.ErrorMessage)).Trim();
    }

    internal class CommonError
    {
        [JsonProperty("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}
