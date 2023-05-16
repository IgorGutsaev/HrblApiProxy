using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class CatalogResult
    {
        [JsonPropertyName("ProdCatalogDetails")]
        public ProdCatalogDetails CatalogDetails { get; set; }
    }

    internal class ProdCatalogDetails
    {
        [JsonPropertyName("ProdCatalogDetail")]
        public CatalogItem[] Items { get; set; }
    }

    public class CatalogItem
    {
        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("SellingSKU")]
        public string SellingSKU { get; set; }

        [JsonPropertyName("EarnBase")]
        public string EarnBase { get; set; }

        [JsonPropertyName("UnitTaxBase")]
        public string UnitTaxBase { get; set; }

        [JsonPropertyName("ProductUOMCode")]
        public string ProductUOMCode { get; set; }

        [JsonPropertyName("ListPrice")]
        public decimal ListPrice { get; set; }

        [JsonPropertyName("VolumePoints")]
        public decimal VolumePoints { get; set; }

        [JsonPropertyName("PlEffStartDt")]
        public DateTime PlEffStartDt { get; set; }

        [JsonPropertyName("PlEffEndDt")]
        public DateTime PlEffEndDt { get; set; }

        [JsonPropertyName("VpEffStartDt")]
        public DateTime VpEffStartDt { get; set; }

        [JsonPropertyName("VpEffEndDt")]
        public DateTime VpEffEndDt { get; set; }

        [JsonPropertyName("CurrencyCode")]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("FreightExFlag")]
        public string FreightExFlag { get; set; }

        [JsonPropertyName("PHExFlag")]
        public string PHExFlag { get; set; }

        [JsonPropertyName("LogisticExFlag")]
        public string LogisticExFlag { get; set; }

        [JsonPropertyName("FlexKitFlag")]
        public string FlexKitFlag { get; set; }

        [JsonPropertyName("IsEventItem")]
        public string IsEventItem { get; set; }

        [JsonPropertyName("SkuRestriction")]
        public string SkuRestriction { get; set; }

        [JsonPropertyName("ROEarnBase")]
        public string ROEarnBase { get; set; }

        [JsonPropertyName("PBEarnBase")]
        public string PBEarnBase { get; set; }

        [JsonPropertyName("HSNSACNumber")]
        public string HSNSACNumber { get; set; }

        [JsonPropertyName("PUM")]
        public string PUM { get; set; }

        [JsonPropertyName("UOM")]
        public string UOM { get; set; }

        [JsonPropertyName("UOMValue")]
        public string UOMValue { get; set; }

        [JsonPropertyName("NumberOfServings")]
        public string NumberOfServings { get; set; }

        [JsonPropertyName("ServingSize")]
        public string ServingSize { get; set; }
    }
}