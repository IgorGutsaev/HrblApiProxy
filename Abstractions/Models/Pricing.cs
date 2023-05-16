using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class PricingRequest
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; set; }

        [JsonPropertyName("OrderPriceHeader")]
        public PricingRequestHeader Header { get; set; }

        [JsonPropertyName("OrderPriceLines")]
        public PricingRequestLine[] Lines { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class PricingResponse
    {
        [JsonPropertyName("OrderPriceHeader")]
        public PricingResponseHeader Header { get; internal set; }

        [JsonPropertyName("OrderPriceLines")]
        public PricingResponseLine[] Lines { get; internal set; }

        [JsonPropertyName("Errors")]
        internal CommonErrorList Errors { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    #region Header
    public abstract class BasePricingHeader
    {
        [JsonPropertyName("ExternalOrderNumber")]
        [JsonPropertyOrder(order: 1)]
        public string ExternalOrderNumber { get; set; }

        [JsonPropertyName("DistributorId")]
        [JsonPropertyOrder(order: 2)]
        public string DistributorId { get; set; }

        [JsonPropertyName("Warehouse")]
        [JsonPropertyOrder(order: 3)]
        public string Warehouse { get; set; }

        [JsonPropertyName("ProcessingLocation")]
        [JsonPropertyOrder(order: 4)]
        public string ProcessingLocation { get; set; }

        [JsonPropertyName("FreightCode")]
        [JsonPropertyOrder(order: 5)]
        public string FreightCode { get; set; }

        [JsonPropertyName("CountryCode")]
        [JsonPropertyOrder(order: 6)]
        public string CountryCode { get; set; }

        [JsonPropertyName("OrderMonth")]
        [JsonPropertyOrder(order: 7)]
        //[JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; set; }

        [JsonPropertyName("OrderCategory")]
        [JsonPropertyOrder(order: 8)]
        public string OrderCategory { get; set; }

        [JsonPropertyName("OrderType")]
        [JsonPropertyOrder(order: 9)]
        public string OrderType { get; set; }

        [JsonPropertyName("PriceDate")]
        [JsonPropertyOrder(order: 10)]
        //[JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; internal set; } = DateTime.UtcNow;

        [JsonPropertyName("OrderDate")]
        [JsonPropertyOrder(order: 11)]
       // [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; internal set; } = DateTime.UtcNow;

        [JsonPropertyName("CurrencyCode")]
        [JsonPropertyOrder(order: 12)]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("OrderSubType")]
        [JsonPropertyOrder(order: 13)]
        public string OrderSubType { get; set; }

        [JsonPropertyName("PostalCode")]
        [JsonPropertyOrder(order: 14)]
        public string PostalCode { get; set; }

        [JsonPropertyName("City")]
        [JsonPropertyOrder(order: 15)]
        public string City { get; set; }

        [JsonPropertyName("State")]
        [JsonPropertyOrder(order: 16)]
        public string State { get; set; } = string.Empty;

        [JsonPropertyName("Province")]
        [JsonPropertyOrder(order: 17)]
        public string Province { get; set; } = string.Empty;

        [JsonPropertyName("County")]
        [JsonPropertyOrder(order: 18)]
        public string County { get; set; } = string.Empty;

        [JsonPropertyName("Address1")]
        [JsonPropertyOrder(order: 19)]
        public string Address1 { get; set; }

        [JsonPropertyName("Address2")]
        [JsonPropertyOrder(order: 20)]
        public string Address2 { get; set; }

        [JsonPropertyName("Address3")]
        [JsonPropertyOrder(order: 21)]
        public string Address3 { get; set; }

        [JsonPropertyName("Address4")]
        [JsonPropertyOrder(order: 22)]
        public string Address4 { get; set; }

        [JsonPropertyName("OrgID")]
        [JsonPropertyOrder(order: 23)]
        public int OrgID { get; set; } = 259;

        [JsonPropertyName("OrderTypeID")]
        [JsonPropertyOrder(order: 24)]
        public int? OrderTypeID { get; set; } = 2940;

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class PricingRequestHeader : BasePricingHeader
    {
        [JsonPropertyName("OrderSource")]
        public string OrderSource { get; set; }

        /// <summary>
        /// To setup timeshift
        /// </summary>
        /// <param name="date"></param>
        public void SetOrderDate(DateTime date)
        {
            OrderDate = date;
        }

        /// <summary>
        /// To setup timeshift
        /// </summary>
        /// <param name="date"></param>
        public void SetPricingDate(DateTime date)
        {
            PriceDate = date;
        }
    }

    public class PricingResponseHeader : BasePricingHeader
    {
        [JsonPropertyName("DiscountPercent")]
        public int? DiscountPercent { get; set; }

        [JsonPropertyName("VolumePoints")]
        public decimal? VolumePoints { get; set; }

        [JsonPropertyName("TotalFreightCharges")]
        public decimal? TotalFreightCharges { get; set; }

        [JsonPropertyName("TotalPHCharges")]
        public decimal? TotalPHCharges { get; set; }

        [JsonPropertyName("TotalLogisticCharges")]
        public decimal? TotalLogisticCharges { get; set; }

        [JsonPropertyName("TotalOtherCharges")]
        public decimal? TotalOtherCharges { get; set; }

        [JsonPropertyName("TotalTaxAmount")]
        public decimal? TotalTaxAmount { get; set; }

        [JsonPropertyName("TotalRetailAmount")]
        public decimal? TotalRetailAmount { get; set; }

        [JsonPropertyName("TotalOrderAmount")]
        public decimal? TotalOrderAmount { get; set; }

        [JsonPropertyName("TotalDiscountAmount")]
        public decimal? TotalDiscountAmount { get; set; }

        [JsonPropertyName("TotalDue")]
        public decimal? TotalDue { get; set; }

        [JsonPropertyName("TotalProductRetail")]
        public decimal? TotalProductRetail { get; set; }

        [JsonPropertyName("TotalLiteratureRetail")]
        public decimal? TotalLiteratureRetail { get; set; }

        [JsonPropertyName("TotalTaxBreakups")]
        public TaxBreakups TaxBreakup { get; set; }

        /// <summary>
        /// For resubmit purposes
        /// </summary>
        /// <param name="date"></param>
        public void SetOrderDate(DateTime date)
        {
            OrderDate = date;
        }
    }
    #endregion

    #region Line
    public class PricingRequestLine
    {
        [JsonPropertyName("ProcessingLocation")]
        [JsonPropertyOrder(order: 1)]
        public string ProcessingLocation { get; set; }

        [JsonPropertyName("SellingSKU")]
        [JsonPropertyOrder(order: 2)]
        public string Sku { get; set; }

        [JsonPropertyName("ProductType")]
        [JsonPropertyOrder(order: 3)]
        public string ProductType { get; set; } = "P";

        [JsonPropertyName("OrderedQty")]
        [JsonPropertyOrder(order: 4)]
        public decimal Quantity { get; set; }

        public override string ToString() => JsonSerializer.Serialize(this);
    }

    public class PricingResponseLine : PricingRequestLine
    {
        [JsonPropertyName("PriceDate")]
       // [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PriceDate { get; set; }

        [JsonPropertyName("LineVolumePoints")]
        public decimal? LineVolumePoints { get; set; }

        [JsonPropertyName("TotalEarnBase")]
        public decimal? TotalEarnBase { get; set; }

        [JsonPropertyName("TotalRetailPrice")]
        public decimal? TotalRetailPrice { get; set; }

        [JsonPropertyName("UnitRetailPrice")]
        public decimal? UnitRetailPrice { get; set; }

        /// <summary>
        /// Without taxes
        /// </summary>
        [JsonPropertyName("LineTotalAmount")]
        public decimal? LineTotalAmount { get; set; }

        /// <summary>
        /// Final amount (with taxes)
        /// </summary>
        [JsonPropertyName("LineDueAmount")]
        public decimal? LineDueAmount { get; set; }

        [JsonPropertyName("LineFreightCharges")]
        public decimal? LineFreightCharges { get; set; }

        [JsonPropertyName("LinePHCharges")]
        public decimal? LinePHCharges { get; set; }

        [JsonPropertyName("LineLogisticCharges")]
        public decimal? LineLogisticCharges { get; set; }

        [JsonPropertyName("LineOtherCharges")]
        public decimal? LineOtherCharges { get; set; }

        [JsonPropertyName("LineMiscCharges")]
        public decimal? LineMiscCharges { get; set; }

        [JsonPropertyName("LineTaxAmount")]
        public decimal? LineTaxAmount { get; set; }

        [JsonPropertyName("LineDiscountAmount")]
        public decimal? LineDiscountAmount { get; set; }

        [JsonPropertyName("Earnbase")]
        public decimal? Earnbase { get; set; }

        [JsonPropertyName("UnitVolumePoints")]
        public decimal? UnitVolumePoints { get; set; }

        [JsonPropertyName("LineDiscountPrice")]
        public decimal? LineDiscountPrice { get; set; }
    }
    #endregion

    public class TaxBreakupItem
    {
        [JsonPropertyName("TaxName")]
        public string Name { get; set; }

        [JsonPropertyName("TaxValue")]
        public decimal? Value { get; set; }

        [JsonPropertyName("TaxRate")]
        public decimal? Rate { get; set; }
    }

    public class TaxBreakups
    {
        [JsonPropertyName("TaxBreakup")]
        public TaxBreakupItem[] Breakups { get; set; }
    }
}