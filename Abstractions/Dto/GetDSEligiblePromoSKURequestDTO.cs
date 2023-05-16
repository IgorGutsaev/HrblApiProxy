using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class GetDSEligiblePromoSKURequestDTO
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; set; }

        [JsonPropertyName("RequestingService")]
        public string RequestingService { get; set; }

        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("OrderMonth")]
        public string OrderMonth { get; set; }

        public DateTime OrderDate { get; set; }

        [JsonPropertyName("OrderType")]
        public string OrderType { get; set; }

        [JsonPropertyName("VolumePoints")]
        public string VolumePoints { get; set; }

        [JsonPropertyName("OrderLines")]
        public OrderLines OrderLines { get; set; }
    }

    public class OrderLines
    {
        [JsonPropertyName("Promotion")]
        public List<ReqPromotion> Promotion { get; set; }
    }

    public class ReqPromotion
    {
        [JsonPropertyName("SKU")]
        public string SKU { get; set; }

        [JsonPropertyName("FreightCode")]
        public string FreightCode { get; set; }

        [JsonPropertyName("OrderedQuantity")]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("ChrAttribute1")]
        public string ChrAttribute1 { get; set; }

        [JsonPropertyName("ChrAttribute2")]
        public string ChrAttribute2 { get; set; }

        /// <summary>
        /// Warehouse code
        /// </summary>
        [JsonPropertyName("ChrAttribute3")]
        public string ChrAttribute3 { get; set; }

        /// <summary>
        /// Product type P/A/L
        /// </summary>
        [JsonPropertyName("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }

        [JsonPropertyName("TotalRetail")]
        public double TotalRetail { get; set; }
    }
}