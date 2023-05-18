using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class UpdateAddressAndContactsRequest
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; set; }

        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; set; }

        [JsonPropertyName("Address")]
        public DistributorAddressToUpdate Address { get; set; }

        [JsonPropertyName("contact")]
        public DistributorContactToUpdate Contact { get; set; }
    }
}