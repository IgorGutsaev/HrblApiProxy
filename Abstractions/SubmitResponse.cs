using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SubmitResponse
    {
        [JsonPropertyName("OrderStatus")]
        public string OrderStatus { get; set; } //  SUCCESS
        [JsonPropertyName("OrderNumber")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("Errors")]
        internal CommonErrorList Errors { get; private set; }

        [JsonIgnore]
        public bool IsSuccess => string.Equals(OrderStatus, "SUCCESS", StringComparison.InvariantCultureIgnoreCase);

        [JsonIgnore]
        public string ErrorMessage => Errors == null || !Errors.HasErrors ? string.Empty : Errors.ErrorMessage;

        [JsonIgnore]
        public bool IsAlreadyImported => Errors == null || !Errors.HasErrors ? false : Errors.ErrorMessage.ToLower().Contains("importing or imported");
    }
}