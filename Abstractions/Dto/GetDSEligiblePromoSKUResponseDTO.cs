using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class GetDSEligiblePromoSKUResponseDTO
    {
        [JsonPropertyName("IsPromoOrder")]
        public string IsPromoOrder { get; set; }

        [JsonPropertyName("Promotions")]
        public RespPromotions Promotions { get; set; }

        [JsonPropertyName("Errors")]
        public string Errors { get; set; }

        [JsonIgnore]
        public bool IsPromo => string.Equals(IsPromoOrder, "Y", StringComparison.InvariantCultureIgnoreCase) && Promotions?.Promotion.Count > 0;
    }

    public class RespPromotion
    {
        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; set; }

        [JsonPropertyName("OrderMonth")]
        public string OrderMonth { get; set; }

        [JsonPropertyName("OrderCount")]
        public int OrderCount { get; set; }

        [JsonPropertyName("DistributorStatus")]
        public string DistributorStatus { get; set; }

        [JsonPropertyName("DOB")]
        public object DOB { get; set; }

        [JsonPropertyName("Country")]
        public string Country { get; set; }

        [JsonPropertyName("DistributorType")]
        public string DistributorType { get; set; }

        [JsonPropertyName("DistributorSubType")]
        public string DistributorSubType { get; set; }

        [JsonPropertyName("SKU")]
        public string SKU { get; set; }

        [JsonPropertyName("ShippingInstructions")]
        public object ShippingInstructions { get; set; }

        [JsonPropertyName("SKUReward")]
        public string SKUReward { get; set; }

        [JsonPropertyName("ApplicationDate")]
        public object ApplicationDate { get; set; }

        [JsonPropertyName("AnniversaryMonth")]
        public object AnniversaryMonth { get; set; }

        [JsonPropertyName("FreightCode")]
        public object FreightCode { get; set; }

        [JsonPropertyName("PromotionRule")]
        public string PromotionRule { get; set; }

        [JsonPropertyName("Precedence")]
        public object Precedence { get; set; }

        [JsonPropertyName("OrderType")]
        public object OrderType { get; set; }

        [JsonPropertyName("OrderedQuantity")]
        public int OrderedQuantity { get; set; }

        [JsonPropertyName("ChrAttribute1")]
        public string ChrAttribute1 { get; set; }

        [JsonPropertyName("ChrAttribute2")]
        public int? ChrAttribute2 { get; set; }

        [JsonPropertyName("ChrAttribute3")]
        public string ChrAttribute3 { get; set; }

        [JsonPropertyName("ChrAttribute4")]
        public string ChrAttribute4 { get; set; }

        [JsonPropertyName("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }

        [JsonPropertyName("DateAttribute1")]
        public DateTime? DateAttribute1 { get; set; }

        [JsonPropertyName("DateAttribute2")]
        public object DateAttribute2 { get; set; }

        [JsonPropertyName("DateAttribute3")]
        public object DateAttribute3 { get; set; }

        [JsonPropertyName("DateAttribute4")]
        public object DateAttribute4 { get; set; }

        [JsonPropertyName("DateAttribute5")]
        public object DateAttribute5 { get; set; }

        [JsonPropertyName("NumAttribute1")]
        public int? NumAttribute1 { get; set; }

        [JsonPropertyName("NumAttribute2")]
        public int? NumAttribute2 { get; set; }

        [JsonPropertyName("NumAttribute3")]
        public int? NumAttribute3 { get; set; }

        [JsonPropertyName("NumAttribute4")]
        public object NumAttribute4 { get; set; }

        [JsonPropertyName("NumAttribute5")]
        public object NumAttribute5 { get; set; }

        [JsonPropertyName("TotalRetail")]
        public object TotalRetail { get; set; }

        [JsonPropertyName("TotalDiscount")]
        public object TotalDiscount { get; set; }

        [JsonPropertyName("TotalDiscountRetail")]
        public object TotalDiscountRetail { get; set; }

        [JsonPropertyName("TotalTax")]
        public object TotalTax { get; set; }

        [JsonPropertyName("TotalVolume")]
        public object TotalVolume { get; set; }

        [JsonPropertyName("ShippingHandling")]
        public object ShippingHandling { get; set; }

        [JsonPropertyName("TotalDue")]
        public object TotalDue { get; set; }

        [JsonPropertyName("ReferencePMTAmount")]
        public object ReferencePMTAmount { get; set; }

        [JsonPropertyName("ReferencePMTCheckNo")]
        public object ReferencePMTCheckNo { get; set; }

        [JsonPropertyName("RedemptionType")]
        public string RedemptionType { get; set; }

        [JsonPropertyName("RuleID")]
        public string RuleID { get; set; }

        [JsonPropertyName("PromotionItem")]
        public object PromotionItem { get; set; }

        [JsonPropertyName("PromotionRuleName")]
        public string PromotionRuleName { get; set; }

        [JsonPropertyName("PromotionType")]
        public string PromotionType { get; set; }

        [JsonPropertyName("PromotionProp1")]
        public string PromotionProp1 { get; set; }

        [JsonPropertyName("PromotionProp2")]
        public object PromotionProp2 { get; set; }

        [JsonPropertyName("RedemptionFlag")]
        public object RedemptionFlag { get; set; }

        [JsonPropertyName("EligibleFlag")]
        public object EligibleFlag { get; set; }

        [JsonPropertyName("PromoNotification")]
        public string PromoNotification { get; set; }

        [JsonPropertyName("ChrAttribute6")]
        public string ChrAttribute6 { get; set; }

        [JsonPropertyName("ChrAttribute7")]
        public string ChrAttribute7 { get; set; }

        [JsonPropertyName("ChrAttribute8")]
        public object ChrAttribute8 { get; set; }

        [JsonPropertyName("ChrAttribute9")]
        public object ChrAttribute9 { get; set; }

        [JsonPropertyName("ChrAttribute10")]
        public object ChrAttribute10 { get; set; }

        [JsonPropertyName("ChrAttribute11")]
        public object ChrAttribute11 { get; set; }

        [JsonPropertyName("ChrAttribute12")]
        public object ChrAttribute12 { get; set; }

        [JsonPropertyName("ChrAttribute13")]
        public object ChrAttribute13 { get; set; }

        [JsonPropertyName("ChrAttribute14")]
        public object ChrAttribute14 { get; set; }

        [JsonPropertyName("ChrAttribute15")]
        public object ChrAttribute15 { get; set; }

        [JsonPropertyName("DateAttribute6")]
        public object DateAttribute6 { get; set; }

        [JsonPropertyName("DateAttribute7")]
        public object DateAttribute7 { get; set; }

        [JsonPropertyName("DateAttribute8")]
        public object DateAttribute8 { get; set; }

        [JsonPropertyName("DateAttribute9")]
        public object DateAttribute9 { get; set; }

        [JsonPropertyName("DateAttribute10")]
        public object DateAttribute10 { get; set; }

        [JsonPropertyName("NumAttribute6")]
        public object NumAttribute6 { get; set; }

        [JsonPropertyName("NumAttribute7")]
        public object NumAttribute7 { get; set; }

        [JsonPropertyName("NumAttribute8")]
        public object NumAttribute8 { get; set; }

        [JsonPropertyName("NumAttribute9")]
        public object NumAttribute9 { get; set; }

        [JsonPropertyName("NumAttribute10")]
        public object NumAttribute10 { get; set; }

        [JsonPropertyName("NumAttribute11")]
        public object NumAttribute11 { get; set; }

        [JsonPropertyName("NumAttribute12")]
        public object NumAttribute12 { get; set; }

        [JsonPropertyName("NumAttribute13")]
        public object NumAttribute13 { get; set; }

        [JsonPropertyName("NumAttribute14")]
        public object NumAttribute14 { get; set; }

        [JsonPropertyName("NumAttribute15")]
        public object NumAttribute15 { get; set; }
    }

    public class RespPromotions
    {
        [JsonPropertyName("Promotion")]
        public List<RespPromotion> Promotion { get; set; }
    }
}
