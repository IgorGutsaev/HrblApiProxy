using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class UpdateAddressAndContactsRequest
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; internal set; }

        [JsonPropertyName("Address")]
        public DistributorAddressToUpdate Address { get; internal set; }

        [JsonPropertyName("contact")]
        public DistributorContactToUpdate Contact { get; internal set; }
    }
}