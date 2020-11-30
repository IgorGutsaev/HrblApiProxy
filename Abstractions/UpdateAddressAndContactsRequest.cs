using Newtonsoft.Json;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class UpdateAddressAndContactsRequest
    {
        [JsonProperty("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonProperty("DistributorId")]
        public string DistributorId { get; internal set; }

        [JsonProperty("Address")]
        public DistributorAddressToUpdate Address { get; internal set; }

        [JsonProperty("contact")]
        public DistributorContactToUpdate Contact { get; internal set; }
    }
}
