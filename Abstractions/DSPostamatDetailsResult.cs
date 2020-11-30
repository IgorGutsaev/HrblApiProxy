using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class DSPostamatDetailsResult
    {
        [JsonProperty("DsPostamatDetails")]
        public DsPostamatDetails[] DsPostamatDetails { get; private set; }

        [JsonProperty("Errors")]
        public CommonErrorList Errors { get; private set; }
    }

    public class DsPostamatDetails
    {
        [JsonProperty("PostamatId")]
        public string PostamatId { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("Region")]
        public string Region { get; set; }

        [JsonProperty("Zipcode")]
        public string Zipcode { get; set; }

        [JsonProperty("FrieghtCode")]
        public string FrieghtCode { get; set; }

        [JsonProperty("ShippingWarehouse")]
        public string ShippingWarehouse { get; set; }

        [JsonProperty("PlaceName")]
        public string PlaceName { get; set; }

        [JsonProperty("MetroStation")]
        public string MetroStation { get; set; }

        [JsonProperty("Street")]
        public string Street { get; set; }

        [JsonProperty("Building")]
        public string Building { get; set; }

        [JsonProperty("AddtionalInfo")]
        public string AddtionalInfo { get; set; }
    }
}
