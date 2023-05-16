using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class CatalogResult
    {
        [JsonPropertyName("ProdCatalogDetails")]
        public ProdCatalogDetails CatalogDetails { get; private set; }
    }

    internal class ProdCatalogDetails
    {
        [JsonPropertyName("ProdCatalogDetail")]
        public CatalogItem[] Items { get; private set; }
    }

    public class CatalogItem
    {
        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; private set; }

        [JsonPropertyName("SellingSKU")]
        public string SellingSKU { get; private set; }

        [JsonPropertyName("EarnBase")]
        public string EarnBase { get; private set; }

        [JsonPropertyName("UnitTaxBase")]
        public string UnitTaxBase { get; private set; }

        [JsonPropertyName("ProductUOMCode")]
        public string ProductUOMCode { get; private set; }

        [JsonPropertyName("ListPrice")]
        public decimal ListPrice { get; private set; }

        [JsonPropertyName("VolumePoints")]
        public decimal VolumePoints { get; private set; }

        [JsonPropertyName("PlEffStartDt")]
        public DateTime PlEffStartDt { get; private set; }

        [JsonPropertyName("PlEffEndDt")]
        public DateTime PlEffEndDt { get; private set; }

        [JsonPropertyName("VpEffStartDt")]
        public DateTime VpEffStartDt { get; private set; }

        [JsonPropertyName("VpEffEndDt")]
        public DateTime VpEffEndDt { get; private set; }

        [JsonPropertyName("CurrencyCode")]
        public string CurrencyCode { get; private set; }

        [JsonPropertyName("FreightExFlag")]
        public string FreightExFlag { get; private set; }

        [JsonPropertyName("PHExFlag")]
        public string PHExFlag { get; private set; }

        [JsonPropertyName("LogisticExFlag")]
        public string LogisticExFlag { get; private set; }

        [JsonPropertyName("FlexKitFlag")]
        public string FlexKitFlag { get; private set; }

        [JsonPropertyName("IsEventItem")]
        public string IsEventItem { get; private set; }

        [JsonPropertyName("SkuRestriction")]
        public string SkuRestriction { get; private set; }

        [JsonPropertyName("ROEarnBase")]
        public string ROEarnBase { get; private set; }

        [JsonPropertyName("PBEarnBase")]
        public string PBEarnBase { get; private set; }

        [JsonPropertyName("HSNSACNumber")]
        public string HSNSACNumber { get; private set; }

        [JsonPropertyName("PUM")]
        public string PUM { get; private set; }

        [JsonPropertyName("UOM")]
        public string UOM { get; private set; }

        [JsonPropertyName("UOMValue")]
        public string UOMValue { get; private set; }

        [JsonPropertyName("NumberOfServings")]
        public string NumberOfServings { get; private set; }

        [JsonPropertyName("ServingSize")]
        public string ServingSize { get; private set; }
    }
}