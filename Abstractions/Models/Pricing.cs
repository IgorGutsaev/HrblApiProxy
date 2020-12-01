﻿using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Newtonsoft.Json;
using System;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class PricingRequest
    {
        [JsonProperty("ServiceConsumer")]
        internal string ServiceConsumer { get; set; }

        [JsonProperty("OrderPriceHeader")]
        internal PricingRequestHeader Header { get; set; }

        [JsonProperty("OrderPriceLines")]
        internal PricingRequestLine[] Lines { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class PricingResponse
    {
        [JsonProperty("OrderPriceHeader")]
        internal PricingResponseHeader Header { get; set; }

        [JsonProperty("OrderPriceLines")]
        internal PricingResponseLine[] Lines { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    #region Header
    public abstract class BasePricingHeader
    {
        [JsonProperty("ExternalOrderNumber", Order = 1)]
        public string ExternalOrderNumber { get; internal set; }

        [JsonProperty("DistributorId", Order = 2)]
        public string DistributorId { get; internal set; }

        [JsonProperty("Warehouse", Order = 3)]
        public string Warehouse { get; internal set; }

        [JsonProperty("ProcessingLocation", Order = 4)]
        public string ProcessingLocation { get; internal set; }

        [JsonProperty("FreightCode", Order = 5)]
        public string FreightCode { get; internal set; }

        [JsonProperty("CountryCode", Order = 6)]
        public string CountryCode { get; internal set; }

        [JsonProperty("OrderMonth", Order = 7)]
        [JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; internal set; }

        [JsonProperty("OrderCategory", Order = 8)]
        public string OrderCategory { get; internal set; }

        [JsonProperty("OrderType", Order = 9)]
        public string OrderType { get; internal set; }

        [JsonProperty("PriceDate", Order = 10)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; internal set; }

        [JsonProperty("OrderDate", Order = 11)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; internal set; }

        [JsonProperty("CurrencyCode", Order = 12)]
        public string CurrencyCode { get; internal set; }

        [JsonProperty("PostalCode", Order = 13)]
        public string PostalCode { get; internal set; }

        [JsonProperty("City", Order = 14)]
        public string City { get; internal set; }

        [JsonProperty("State", Order = 15)]
        public string State { get; internal set; } = string.Empty;

        [JsonProperty("Address1", Order = 16)]
        public string Address1 { get; internal set; }

        [JsonProperty("Address2", Order = 17)]
        public string Address2 { get; internal set; }

        [JsonProperty("Address3", Order = 18)]
        public string Address3 { get; internal set; }

        [JsonProperty("Address4", Order = 19)]
        public string Address4 { get; internal set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class PricingRequestHeader : BasePricingHeader
    {
        [JsonProperty("OrderSource")]
        public string OrderSource { get; internal set; }
    }

    public class PricingResponseHeader : BasePricingHeader
    {
        [JsonProperty("DiscountPercent")]
        public int DiscountPercent { get; private set; }

        [JsonProperty("VolumePoints")]
        public decimal VolumePoints { get; private set; }

        [JsonProperty("TotalFreightCharges")]
        public decimal TotalFreightCharges { get; private set; }

        [JsonProperty("TotalPHCharges")]
        public decimal TotalPHCharges { get; private set; }

        [JsonProperty("TotalLogisticCharges")]
        public decimal TotalLogisticCharges { get; private set; }

        [JsonProperty("TotalOtherCharges")]
        public decimal TotalOtherCharges { get; private set; }

        [JsonProperty("TotalTaxAmount")]
        public decimal TotalTaxAmount { get; private set; }

        [JsonProperty("TotalRetailAmount")]
        public decimal TotalRetailAmount { get; private set; }

        [JsonProperty("TotalOrderAmount")]
        public decimal TotalOrderAmount { get; private set; }

        [JsonProperty("TotalDiscountAmount")]
        public decimal TotalDiscountAmount { get; private set; }

        [JsonProperty("TotalDue")]
        public decimal TotalDue { get; private set; }

        [JsonProperty("TotalProductRetail")]
        public decimal TotalProductRetail { get; private set; }

        [JsonProperty("TotalLiteratureRetail")]
        public decimal TotalLiteratureRetail { get; private set; }
    }
    #endregion

    #region Line
    public class PricingRequestLine
    {
        [JsonProperty("ProcessingLocation", Order = 1)]
        public string ProcessingLocation { get; internal set; }

        [JsonProperty("SellingSKU", Order = 2)]
        public string Sku { get; internal set; }

        [JsonProperty("ProductType", Order = 3)]
        public string ProductType { get; internal set; } = "P";

        [JsonProperty("OrderedQty", Order = 4)]
        public decimal Quantity { get; internal set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }

    public class PricingResponseLine : PricingRequestLine
    {
        [JsonProperty("PriceDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; private set; }

        [JsonProperty("LineVolumePoints")]
        public decimal LineVolumePoints { get; private set; }

        [JsonProperty("TotalEarnBase")]
        public decimal TotalEarnBase { get; private set; }

        [JsonProperty("TotalRetailPrice")]
        public decimal TotalRetailPrice { get; private set; }

        [JsonProperty("UnitRetailPrice")]
        public decimal UnitRetailPrice { get; private set; }

        [JsonProperty("LineTotalAmount")]
        public decimal LineTotalAmount { get; private set; }

        [JsonProperty("LineDueAmount")]
        public decimal LineDueAmount { get; private set; }

        [JsonProperty("LineFreightCharges")]
        public decimal LineFreightCharges { get; private set; }

        [JsonProperty("LinePHCharges")]
        public decimal LinePHCharges { get; private set; }

        [JsonProperty("LineLogisticCharges")]
        public decimal LineLogisticCharges { get; private set; }

        [JsonProperty("LineOtherCharges")]
        public decimal LineOtherCharges { get; private set; }

        [JsonProperty("LineMiscCharges")]
        public decimal LineMiscCharges { get; private set; }

        [JsonProperty("LineTaxAmount")]
        public decimal LineTaxAmount { get; private set; }

        [JsonProperty("LineDiscountAmount")]
        public decimal LineDiscountAmount { get; private set; }

        [JsonProperty("Earnbase")]
        public decimal Earnbase { get; private set; }

        [JsonProperty("UnitVolumePoints")]
        public decimal UnitVolumePoints { get; private set; }

        [JsonProperty("LineDiscountPrice")]
        public decimal LineDiscountPrice { get; private set; }
    }
    #endregion
}
