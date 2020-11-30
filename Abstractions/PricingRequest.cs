using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class PricingRequest
    {
        [JsonProperty("ServiceConsumer")]
        public string ServiceConsumer { get; private set; }

        [JsonProperty("OrderPriceHeader")]
        public OrderPriceHeader Header { get; private set; }

        [JsonProperty("OrderPriceLines")]
        public OrderPriceLine[] Items { get; private set; }
    }

    public class OrderPriceHeader
    {
        [JsonProperty("ProcessingLocation")]
        public string ProcessingLocation { get; private set; }

        [JsonProperty("ExternalOrderNumber")]
        public string ExternalOrderNumber { get; private set; }

        [JsonProperty("DistributorId")]
        public string DistributorId { get; private set; }

        [JsonProperty("Warehouse")]
        public string Warehouse { get; private set; }

        [JsonProperty("OrderMonth")]
        public string OrderMonth { get; private set; }

        [JsonProperty("FreightCode")]
        public string FreightCode { get; private set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; private set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; private set; }

        [JsonProperty("City")]
        public string City { get; private set; }

        [JsonProperty("OrderCategory")]
        public string OrderCategory { get; private set; }

        [JsonProperty("OrderType")]
        public string OrderType { get; private set; }

        [JsonProperty("PriceDate")]
        public DateTime PriceDate { get; private set; }

        [JsonProperty("OrderDate")]
        public DateTime OrderDate { get; private set; }

        [JsonProperty("InvoicetoCode")]
        public string InvoicetoCode { get; private set; }

        [JsonProperty("PriceListHeaderId")]
        public string PriceListHeaderId { get; private set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; private set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; private set; }

        [JsonProperty("Address1")]
        public string Address1 { get; private set; }
    }

    public class OrderPriceLine
    {
        [JsonProperty("LineNumber")]
        public UInt16 LineNumber { get; private set; }

        [JsonProperty("ExternalOrderNumber")]
        public string ExternalOrderNumber { get; private set; }

        [JsonProperty("ProcessingLocation")]
        public string ProcessingLocation { get; private set; }

        [JsonProperty("SellingSKU")]
        public string SellingSKU { get; private set; }

        [JsonProperty("ProductType")]
        public string ProductType { get; private set; }

        [JsonProperty("UOM")]
        public string UOM { get; private set; }

        [JsonProperty("OrderedQty")]
        public UInt16 OrderedQty { get; private set; }

        [JsonProperty("CurrencyCode")]
        public string CurrencyCode { get; private set; }
    }
}
