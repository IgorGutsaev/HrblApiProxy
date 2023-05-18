using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class GetDistributorTinsResult
    {
        [JsonPropertyName("DistributorId")]
        public string Distributor { get; set; }

        [JsonPropertyName("TinDetails")]
        public TinDetails TinDetails { get; set; }

        [JsonPropertyName("ErrorDetails")] // Also might be ranamed as Errors
        public CommonErrorList Errors { get; set; }
    }

    public class TinDetails
    {
        [JsonPropertyName("DistributorTin")]
        public DistributorTin[] DistributorTins { get; set; }
    }

    public class DistributorTin
    {
        [JsonPropertyName("TinCode")]
        public string Code { get; set; }
                
        [JsonPropertyName("TinNumber")]
        public string Number { get; set; }

        [JsonPropertyName("TinCountry")]
        public string Country { get; set; }

        [JsonPropertyName("EffectiveDate")]
        public string EffectiveDate { get; set; }

        [JsonPropertyName("ExpirationDate")]
        public string ExpirationDate { get; set; }

        [JsonPropertyName("ActiveFlag")]
        internal string _isActive { get; set; }

        [JsonIgnore]
        public bool IsActive => string.Equals(_isActive, "Y", StringComparison.InvariantCultureIgnoreCase);
    }
}