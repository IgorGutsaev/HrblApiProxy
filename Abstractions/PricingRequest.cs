using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class PricingRequest
    {
        [JsonProperty("ServiceConsumer")]
        internal string ServiceConsumer { get; set; }

        [JsonProperty("OrderPriceHeader")]
        internal OrderPriceHeader Header { get; set; }

        [JsonProperty("OrderPriceLines")]
        internal OrderPriceLine[] Lines { get; set; }
    }

    public class OrderPriceHeader
    {
        [JsonProperty("ProcessingLocation")]
        public string ProcessingLocation { get; internal set; }


        [JsonProperty("ExternalOrderNumber")]
        public string ExternalOrderNumber { get; internal set; }

        [JsonProperty("OrderSource")]
        public string OrderSource { get; internal set; }

        [JsonProperty("DistributorId")]
        public string DistributorId { get; internal set; }

        [JsonProperty("Warehouse")]
        public string Warehouse { get; internal set; }

        [JsonProperty("OrderMonth")]
        [JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; internal set; }

        [JsonProperty("FreightCode")]
        public string FreightCode { get; internal set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; internal set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; internal set; }

        [JsonProperty("City")]
        public string City { get; internal set; }

        [JsonProperty("State")]
        public string State { get; internal set; } = string.Empty;

        [JsonProperty("OrderCategory")]
        public string OrderCategory { get; internal set; }

        [JsonProperty("OrderType")]
        public string OrderType { get; internal set; }

        [JsonProperty("PriceDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; internal set; }

        [JsonProperty("OrderDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; internal set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; internal set; }

        //[JsonProperty("CustomerName")]
        //public string CustomerName { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; internal set; }

        [JsonProperty("Address2")]
        public string Address2 { get; internal set; }

        [JsonProperty("Address3")]
        public string Address3 { get; internal set; }

        [JsonProperty("Address4")]
        public string Address4 { get; internal set; }
    }

    public class OrderPriceLine
    {
        //[JsonProperty("LineNumber")]
        //public UInt16 LineNumber { get; printernalivate set; }

        //[JsonProperty("ExternalOrderNumber")]
        //public string ExternalOrderNumber { get; internal set; }

        [JsonProperty("ProcessingLocation")]
        public string ProcessingLocation { get; internal set; }

        [JsonProperty("SellingSKU")]
        public string Sku { get; internal set; }

        [JsonProperty("ProductType")]
        public string ProductType { get; internal set; } = "P";

        //[JsonProperty("UOM")]
        //public string UOM { get; private set; }

        [JsonProperty("OrderedQty")]
        public decimal Quantity { get; internal set; }

        [JsonProperty("PriceDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; internal set; }
    }
}
