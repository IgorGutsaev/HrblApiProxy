using Newtonsoft.Json;
using System;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class GetDistributorTinsResult
    {
        [JsonProperty("DistributorId")]
        private string Distributor { get; set; }

        [JsonProperty("TinDetails")]
        public TinDetails TinDetails { get; set; }

        [JsonProperty("ErrorDetails")] // Also might be ranamed as Errors
        internal CommonErrorList Errors { get; private set; }
    }

    public class TinDetails
    {
        [JsonProperty("DistributorTin")]
        public DistributorTin[] DistributorTins { get; set; }
    }

    public class DistributorTin
    {
        [JsonProperty("TinCode")]
        private string Code { get; set; }
                
        [JsonProperty("TinNumber")]
        private string Number { get; set; }

        [JsonProperty("TinCountry")]
        private string Country { get; set; }

        [JsonProperty("EffectiveDate")]
        private string TinNumber { get; set; }

        [JsonProperty("ExpirationDate")]
        private string ExpirationDate { get; set; }


        [JsonProperty("ActiveFlag")]
        internal string _isActive { get; set; }

        [JsonIgnore]
        public bool IsActive => string.Equals(_isActive, "Y", StringComparison.InvariantCultureIgnoreCase);
    }
}