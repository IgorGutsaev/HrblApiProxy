using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class GetDSEligiblePromoSKURequestDTO
    {
        [JsonProperty("ServiceConsumer")]
        public string ServiceConsumer { get; set; }

        [JsonProperty("RequestingService")]
        public string RequestingService { get; set; }

        [JsonProperty("DistributorId")]
        public string DistributorId { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("OrderMonth")]
        public string OrderMonth { get; set; }

        public DateTime OrderDate { get; set; }

        [JsonProperty("OrderType")]
        public string OrderType { get; set; }

        [JsonProperty("VolumePoints")]
        public string VolumePoints { get; set; }

        [JsonProperty("OrderLines")]
        public OrderLines OrderLines { get; set; }
    }

    public class OrderLines
    {
        [JsonProperty("Promotion")]
        public List<ReqPromotion> Promotion { get; set; }
    }

    public class ReqPromotion
    {
        [JsonProperty("SKU")]
        public string SKU { get; set; }

        [JsonProperty("FreightCode")]
        public string FreightCode { get; set; }

        [JsonProperty("OrderedQuantity")]
        public int OrderedQuantity { get; set; }

        [JsonProperty("ChrAttribute1")]
        public string ChrAttribute1 { get; set; }

        [JsonProperty("ChrAttribute2")]
        public string ChrAttribute2 { get; set; }

        /// <summary>
        /// Warehouse code
        /// </summary>
        [JsonProperty("ChrAttribute3")]
        public string ChrAttribute3 { get; set; }

        /// <summary>
        /// Product type P/A/L
        /// </summary>
        [JsonProperty("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }

        [JsonProperty("TotalRetail")]
        public double TotalRetail { get; set; }
    }
}
