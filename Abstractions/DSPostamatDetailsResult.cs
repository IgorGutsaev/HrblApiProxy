using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class DSPostamatDetailsResult
    {
        [JsonPropertyName("DsPostamatDetails")]
        public DsPostamatDetails[] DsPostamatDetails { get; set; }

        [JsonPropertyName("Errors")]
        public CommonErrorList Errors { get; set; }
    }

    public class DsPostamatDetails
    {
        [JsonPropertyName("PostamatId")]
        public string PostamatId { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("Region")]
        public string Region { get; set; }

        [JsonPropertyName("Zipcode")]
        public string Zipcode { get; set; }

        [JsonPropertyName("FrieghtCode")]
        public string FrieghtCode { get; set; }

        [JsonPropertyName("ShippingWarehouse")]
        public string ShippingWarehouse { get; set; }

        [JsonPropertyName("PlaceName")]
        public string PlaceName { get; set; }

        [JsonPropertyName("MetroStation")]
        public string MetroStation { get; set; }

        [JsonPropertyName("Street")]
        public string Street { get; set; }

        [JsonPropertyName("Building")]
        public string Building { get; set; }

        [JsonPropertyName("AddtionalInfo")]
        public string AddtionalInfo { get; set; }
    }
}