using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class CatalogResult
    {
        [JsonProperty("ProdCatalogDetails")]
        public ProdCatalogDetails CatalogDetails { get; private set; }
    }

    internal class ProdCatalogDetails
    {
        [JsonProperty("ProdCatalogDetail")]
        public CatalogItem[] Items { get; private set; }
    }

    public class CatalogItem
    {
        [JsonProperty("CountryCode")]
        public string CountryCode { get; private set; }

        [JsonProperty("SellingSKU")]
        public string SellingSKU { get; private set; }

        [JsonProperty("EarnBase")]
        public string EarnBase { get; private set; }

        [JsonProperty("UnitTaxBase")]
        public string UnitTaxBase { get; private set; }

        [JsonProperty("ProductUOMCode")]
        public string ProductUOMCode { get; private set; }

        [JsonProperty("ListPrice")]
        public decimal ListPrice { get; private set; }

        [JsonProperty("VolumePoints")]
        public decimal VolumePoints { get; private set; }

        [JsonProperty("PlEffStartDt")]
        public DateTime PlEffStartDt { get; private set; }

        [JsonProperty("PlEffEndDt")]
        public DateTime PlEffEndDt { get; private set; }

        [JsonProperty("VpEffStartDt")]
        public DateTime VpEffStartDt { get; private set; }

        [JsonProperty("VpEffEndDt")]
        public DateTime VpEffEndDt { get; private set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; private set; }

        [JsonProperty("FreightExFlag")]
        public string FreightExFlag { get; private set; }

        [JsonProperty("PHExFlag")]
        public string PHExFlag { get; private set; }

        [JsonProperty("LogisticExFlag")]
        public string LogisticExFlag { get; private set; }

        [JsonProperty("FlexKitFlag")]
        public string FlexKitFlag { get; private set; }

        [JsonProperty("IsEventItem")]
        public string IsEventItem { get; private set; }

        [JsonProperty("SkuRestriction")]
        public string SkuRestriction { get; private set; }

        [JsonProperty("ROEarnBase")]
        public string ROEarnBase { get; private set; }

        [JsonProperty("PBEarnBase")]
        public string PBEarnBase { get; private set; }

        [JsonProperty("HSNSACNumber")]
        public string HSNSACNumber { get; private set; }

        [JsonProperty("PUM")]
        public string PUM { get; private set; }

        [JsonProperty("UOM")]
        public string UOM { get; private set; }

        [JsonProperty("UOMValue")]
        public string UOMValue { get; private set; }

        [JsonProperty("NumberOfServings")]
        public string NumberOfServings { get; private set; }

        [JsonProperty("ServingSize")]
        public string ServingSize { get; private set; }
    }
}
