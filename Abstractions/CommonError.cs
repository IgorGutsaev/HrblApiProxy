using System.Linq;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class CommonErrorList
    {
        [JsonPropertyName("Error")]
        public CommonError[] Error { get; set; } = new CommonError[0];

        [JsonIgnore]
        public bool HasErrors => Error != null && Error.Any(x => x.ErrorCode != "0");

        [JsonIgnore]
        public string ErrorMessage => !HasErrors ? string.Empty :
            string.Join("\n",Error.Where(x => x.ErrorCode != "0").Select(x => x.ErrorMessage)).Trim();
    }

    internal class CommonError
    {
        [JsonPropertyName("ErrorCode")]
        public string ErrorCode { get; set; }

        [JsonPropertyName("ErrorMessage")]
        public string ErrorMessage { get; set; }
    }
}