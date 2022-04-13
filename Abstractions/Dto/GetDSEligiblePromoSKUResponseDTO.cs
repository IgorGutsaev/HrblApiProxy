using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Dto
{
    public class GetDSEligiblePromoSKUResponseDTO
    {
        [JsonProperty("IsPromoOrder")]
        public string IsPromoOrder { get; set; }

        [JsonProperty("Promotions")]
        public RespPromotions Promotions { get; set; }

        [JsonProperty("Errors")]
        public string Errors { get; set; }

        [JsonIgnore]
        public bool IsPromo => string.Equals(IsPromoOrder, "Y", StringComparison.InvariantCultureIgnoreCase) && Promotions.Promotion.Count > 0;
    }

    public class RespPromotion
    {
        [JsonProperty("DistributorId")]
        public string DistributorId { get; set; }

        [JsonProperty("OrderMonth")]
        public string OrderMonth { get; set; }

        [JsonProperty("OrderCount")]
        public int OrderCount { get; set; }

        [JsonProperty("DistributorStatus")]
        public string DistributorStatus { get; set; }

        [JsonProperty("DOB")]
        public object DOB { get; set; }

        [JsonProperty("Country")]
        public string Country { get; set; }

        [JsonProperty("DistributorType")]
        public string DistributorType { get; set; }

        [JsonProperty("DistributorSubType")]
        public string DistributorSubType { get; set; }

        [JsonProperty("SKU")]
        public string SKU { get; set; }

        [JsonProperty("ShippingInstructions")]
        public object ShippingInstructions { get; set; }

        [JsonProperty("SKUReward")]
        public string SKUReward { get; set; }

        [JsonProperty("ApplicationDate")]
        public object ApplicationDate { get; set; }

        [JsonProperty("AnniversaryMonth")]
        public object AnniversaryMonth { get; set; }

        [JsonProperty("FreightCode")]
        public object FreightCode { get; set; }

        [JsonProperty("PromotionRule")]
        public string PromotionRule { get; set; }

        [JsonProperty("Precedence")]
        public object Precedence { get; set; }

        [JsonProperty("OrderType")]
        public object OrderType { get; set; }

        [JsonProperty("OrderedQuantity")]
        public int OrderedQuantity { get; set; }

        [JsonProperty("ChrAttribute1")]
        public string ChrAttribute1 { get; set; }

        [JsonProperty("ChrAttribute2")]
        public object ChrAttribute2 { get; set; }

        [JsonProperty("ChrAttribute3")]
        public string ChrAttribute3 { get; set; }

        [JsonProperty("ChrAttribute4")]
        public string ChrAttribute4 { get; set; }

        [JsonProperty("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }

        [JsonProperty("DateAttribute1")]
        public DateTime? DateAttribute1 { get; set; }

        [JsonProperty("DateAttribute2")]
        public object DateAttribute2 { get; set; }

        [JsonProperty("DateAttribute3")]
        public object DateAttribute3 { get; set; }

        [JsonProperty("DateAttribute4")]
        public object DateAttribute4 { get; set; }

        [JsonProperty("DateAttribute5")]
        public object DateAttribute5 { get; set; }

        [JsonProperty("NumAttribute1")]
        public int? NumAttribute1 { get; set; }

        [JsonProperty("NumAttribute2")]
        public int? NumAttribute2 { get; set; }

        [JsonProperty("NumAttribute3")]
        public int? NumAttribute3 { get; set; }

        [JsonProperty("NumAttribute4")]
        public object NumAttribute4 { get; set; }

        [JsonProperty("NumAttribute5")]
        public object NumAttribute5 { get; set; }

        [JsonProperty("TotalRetail")]
        public object TotalRetail { get; set; }

        [JsonProperty("TotalDiscount")]
        public object TotalDiscount { get; set; }

        [JsonProperty("TotalDiscountRetail")]
        public object TotalDiscountRetail { get; set; }

        [JsonProperty("TotalTax")]
        public object TotalTax { get; set; }

        [JsonProperty("TotalVolume")]
        public object TotalVolume { get; set; }

        [JsonProperty("ShippingHandling")]
        public object ShippingHandling { get; set; }

        [JsonProperty("TotalDue")]
        public object TotalDue { get; set; }

        [JsonProperty("ReferencePMTAmount")]
        public object ReferencePMTAmount { get; set; }

        [JsonProperty("ReferencePMTCheckNo")]
        public object ReferencePMTCheckNo { get; set; }

        [JsonProperty("RedemptionType")]
        public string RedemptionType { get; set; }

        [JsonProperty("RuleID")]
        public string RuleID { get; set; }

        [JsonProperty("PromotionItem")]
        public object PromotionItem { get; set; }

        [JsonProperty("PromotionRuleName")]
        public string PromotionRuleName { get; set; }

        [JsonProperty("PromotionType")]
        public string PromotionType { get; set; }

        [JsonProperty("PromotionProp1")]
        public string PromotionProp1 { get; set; }

        [JsonProperty("PromotionProp2")]
        public object PromotionProp2 { get; set; }

        [JsonProperty("RedemptionFlag")]
        public object RedemptionFlag { get; set; }

        [JsonProperty("EligibleFlag")]
        public object EligibleFlag { get; set; }

        [JsonProperty("PromoNotification")]
        public string PromoNotification { get; set; }

        [JsonProperty("ChrAttribute6")]
        public string ChrAttribute6 { get; set; }

        [JsonProperty("ChrAttribute7")]
        public string ChrAttribute7 { get; set; }

        [JsonProperty("ChrAttribute8")]
        public object ChrAttribute8 { get; set; }

        [JsonProperty("ChrAttribute9")]
        public object ChrAttribute9 { get; set; }

        [JsonProperty("ChrAttribute10")]
        public object ChrAttribute10 { get; set; }

        [JsonProperty("ChrAttribute11")]
        public object ChrAttribute11 { get; set; }

        [JsonProperty("ChrAttribute12")]
        public object ChrAttribute12 { get; set; }

        [JsonProperty("ChrAttribute13")]
        public object ChrAttribute13 { get; set; }

        [JsonProperty("ChrAttribute14")]
        public object ChrAttribute14 { get; set; }

        [JsonProperty("ChrAttribute15")]
        public object ChrAttribute15 { get; set; }

        [JsonProperty("DateAttribute6")]
        public object DateAttribute6 { get; set; }

        [JsonProperty("DateAttribute7")]
        public object DateAttribute7 { get; set; }

        [JsonProperty("DateAttribute8")]
        public object DateAttribute8 { get; set; }

        [JsonProperty("DateAttribute9")]
        public object DateAttribute9 { get; set; }

        [JsonProperty("DateAttribute10")]
        public object DateAttribute10 { get; set; }

        [JsonProperty("NumAttribute6")]
        public object NumAttribute6 { get; set; }

        [JsonProperty("NumAttribute7")]
        public object NumAttribute7 { get; set; }

        [JsonProperty("NumAttribute8")]
        public object NumAttribute8 { get; set; }

        [JsonProperty("NumAttribute9")]
        public object NumAttribute9 { get; set; }

        [JsonProperty("NumAttribute10")]
        public object NumAttribute10 { get; set; }

        [JsonProperty("NumAttribute11")]
        public object NumAttribute11 { get; set; }

        [JsonProperty("NumAttribute12")]
        public object NumAttribute12 { get; set; }

        [JsonProperty("NumAttribute13")]
        public object NumAttribute13 { get; set; }

        [JsonProperty("NumAttribute14")]
        public object NumAttribute14 { get; set; }

        [JsonProperty("NumAttribute15")]
        public object NumAttribute15 { get; set; }
    }

    public class RespPromotions
    {
        [JsonProperty("Promotion")]
        public List<RespPromotion> Promotion { get; set; }
    }
}
