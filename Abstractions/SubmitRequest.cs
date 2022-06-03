using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Newtonsoft.Json;
using System;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SubmitRequest
    {
        [JsonProperty("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonProperty("OrderHeaders")]
        public SubmitRequestHeader Header { get; internal set; }

        [JsonProperty("OrderLine")]
        public SubmitRequestOrderLine[] Lines { get; internal set; }

        [JsonProperty("OrderPayment")]
        public SubmitRequestPayment[] Payment { get; internal set; } // 1 mandatory node is payment while optional are Cash Vouchers

        [JsonProperty("OrderNotes")]
        public string OrderNotes { get; internal set; }

        [JsonProperty("OrderAddress")]
        public string OrderAddress { get; internal set; }

        [JsonProperty("OrderPromotionLine")]
        public SubmitRequestOrderPromotionLine[] OrderPromotionLine { get; internal set; }
    }

    public class SubmitRequestHeader
    {
        // Order details mandatory
        [JsonProperty("DistributorId")]
        public string DistributorId { get; set; }

        [JsonProperty("CustomerName")]
        public string CustomerName { get; set; }

        [JsonProperty("ExternalOrderNumber")]
        public string ExternalOrderNumber { get; set; }

        [JsonProperty("TotalDue")]
        public decimal TotalDue { get; set; }

        [JsonProperty("OrderMonth")]
        [JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; set; }

        [JsonProperty("SalesChannelCode")]
        public string SalesChannelCode { get; set; }

        [JsonProperty("OrderDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; set; }

        [JsonProperty("PricingDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PricingDate { get; set; }

        [JsonProperty("TotalVolume")]
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// A.k.a. OrderCategory
        /// </summary>
        [JsonProperty("OrderTypeCode")]
        public string OrderTypeCode { get; set; }

        [JsonProperty("TotalAmountPaid")]
        public decimal TotalAmountPaid { get; set; }

        [JsonProperty("OrderPaymentStatus")]
        public string OrderPaymentStatus { get; set; }

        [JsonProperty("OrderDiscount")]
        public decimal OrderDiscountPercent { get; set; } // DiscountPercent from OrderPriceHeader

        // Location payload mandatory (oracle settings basically)
        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("WareHouseCode")]
        public string WareHouseCode { get; set; }

        [JsonProperty("ProcessingLocation")]
        public string ProcessingLocation { get; set; }

        /// <summary>
        /// A.k.a. freight code
        /// </summary>
        [JsonProperty("ShippingMethodCode")]
        public string ShippingMethodCode { get; set; }

        [JsonProperty("City")]
        public string City { get; set; }

        [JsonProperty("InvShipFlag")]
        public string InvShipFlag { get; set; }

        [JsonProperty("OrgId")]
        [JsonConverter(typeof(StringIntConverter))]
        public int OrgId { get; set; }

        [JsonProperty("OrderTypeId")]
        [JsonConverter(typeof(StringIntConverter))]
        public int OrderTypeId { get; set; }

        [JsonProperty("PostalCode")]
        public string PostalCode { get; set; }

        [JsonProperty("Province")]
        public string Province { get; set; }

        [JsonProperty("Address1")]
        public string Address1 { get; set; }

        [JsonProperty("Address2")]
        public string Address2 { get; set; }

        [JsonProperty("State")]
        public string State { get; set; }

        // Non-mandatory
        [JsonProperty("ShippingInstructions")]
        public string ShippingInstructions { get; set; }

        [JsonProperty("PickupName")]
        public string PickupName { get; set; } // = string.Empty;

        [JsonProperty("TaxAmount")]
        public decimal TaxAmount { get; set; } // Total tax amount

        //[JsonProperty("FreightCharges")]
        //[JsonConverter(typeof(StringDecimalConverter))]
        //public decimal FreightCharges { get; set; }

        [JsonProperty("DiscountAmount")]
        public decimal DiscountAmount { get; set; } = 0m;

        [JsonProperty("OrderConfirmEmail")]
        public string OrderConfirmEmail { get; set; }

        [JsonProperty("WillCallFlag")]
        public string WillCallFlag { get; set; } = "N";

        [JsonProperty("OrderPurpose")]
        public string OrderPurpose { get; set; } // = string.Empty;

        [JsonProperty("SlidingDiscount")]
        public decimal SlidingDiscount { get; set; } = 0m;           // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonProperty("OrderSource")]
        public string OrderSource { get; set; }// =  "KIOSK"   хардкод (переделать на знач по-умолчанию)

        [JsonProperty("Balance")]
        public decimal Balance { get; set; } = 0m;             // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonProperty("TotalRetailPrice")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }         // спорный момент, скорее не нужен	   = isAALatviaStub ? 0 : 99,    bool isAALatviaStub = string.Equals(salesChannelCode, "AUTOATTENDANT", StringComparison.InvariantCultureIgnoreCase);        

        [JsonProperty("ChrAttribute4")]
        public string ChrAttribute4 { get; set; }  //(isPudo ? postamatCode : null)  bool isPudo = freightCode.Equals("BLD") || freightCode.Equals("BLO");

        [JsonProperty("ChrAttribute3")]
        public string ChrAttribute3 { get; set; } = "N";          // isPudo ?"Y":"N"

        [JsonProperty("ChrAttribute6")]
        public string ChrAttribute6 { get; set; } = "QR";            // QR хардкод (для Балтики как минимум)

        [JsonProperty("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }          //(isPudo ? phone : null)

        [JsonProperty("Phone")]
        public string Phone { get; set; }  // null по ум 

        [JsonProperty("Notes")]
        public string Notes { get; set; }            //string     не mandatory (null по-умолчанию)

        [JsonProperty("SMSNumber")]
        public string SMSNumber { get; set; }                // null string

        [JsonProperty("SMSAction")]
        public string SMSAction { get; set; }// = "ORDER COMPLETION";       //"ORDER COMPLETION" по-умолчанию

        [JsonProperty("SMSRole")]
        public string SMSRole { get; set; }// = "DS";           //      "DS" по-умолчанию

        [JsonProperty("OrderSubType")]
        public string OrderSubType { get; set; } // = string.Empty     // "" по-умолчанию
    }

    public class SubmitRequestOrderLine
    {
        [JsonProperty("SKU", Order = 1)]
        public string Sku { get; set; }

        [JsonProperty("Quantity", Order = 2)]
       // [JsonConverter(typeof(StringIntConverter))]
        public decimal Quantity { get; set; }

        [JsonProperty("LineAmount", Order = 3)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal Amount { get; set; }

        [JsonProperty("UnitVolume", Order = 4)]
       // [JsonConverter(typeof(StringDecimalConverter))]
        public decimal UnitVolume { get; set; }

        [JsonProperty("EarnBase", Order = 5)]
       // [JsonConverter(typeof(StringDecimalConverter))]
        public decimal EarnBase { get; set; }

        [JsonProperty("TotalRetailPrice", Order = 6)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }

        [JsonProperty("TotalDiscountedPrice", Order = 7)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalDiscountedPrice { get; set; }

        [JsonProperty("ProductType", Order = 8)]
        public string ProductType { get; set; } = "P";
    }

    public class SubmitRequestPayment
    {
        [JsonProperty("PaymentMethodName", Order = 1)]
        public string PaymentMethodName { get; set; }

        [JsonProperty("PaymentStatus", Order = 2)]
        public string PaymentStatus { get; set; }

        [JsonProperty("PaymentMethodId", Order = 3)]
        public string PaymentMethodId { get; set; }

        [JsonProperty("PaymentAmount", Order = 4)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentAmount { get; set; }

        [JsonProperty("PaymentDate", Order = 5)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime Date { get; set; } // nullable

        [JsonProperty("Paycode", Order = 6)]
        public string Paycode { get; set; } // CARD CASH

        [JsonProperty("PaymentType", Order = 7)]
        public string PaymentType { get; set; }

        [JsonProperty("CurrencyCode", Order = 8)]
        public string CurrencyCode { get; set; }

        [JsonProperty("AppliedDate", Order = 9)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime AppliedDate { get; set; }

        [JsonProperty("ApprovalNumber", Order = 10)]
        public string ApprovalNumber { get; set; }        
        
        [JsonProperty("CheckWireNumber", Order = 11)]
        public string CheckWireNumber { get; set; }

        [JsonProperty("PaymentReceived", Order = 12)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentReceived { get; set; }

        [JsonProperty("CreditCard", Order = 13)]
        public OrderSubmitPaymentCreditCard CreditCard { get; set; } = new OrderSubmitPaymentCreditCard();

        [JsonProperty("AuthorizationType", Order = 14)]
        internal string AuthorizationType { get; set; } = "ONLINE";

        [JsonProperty("VoidFlag", Order = 15)]
        internal string VoidFlag { get; set; } = "N";

        [JsonProperty("ClientRefNumber", Order = 16)]
        public string ClientRefNumber { get; set; }
    }

    public class OrderSubmitPaymentCreditCard
    {
        [JsonProperty("CardNumber", Order = 1)]
        public string CardNumber { get; set; }

        [JsonProperty("TrxApprovalNumber", Order = 2)]
        public string TrxApprovalNumber { get; set; }

        [JsonProperty("CardType", Order = 3)]
        public string CardType { get; set; }

        [JsonProperty("CardExpiryDate", Order = 4)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime CardExpiryDate { get; set; }

        [JsonProperty("CardHolderName", Order = 5)]
        public string CardHolderName { get; set; }

        [JsonProperty("CardValidationType", Order = 6)]
        internal string CardValidationType { get; set; } = "ONLINE";

        [JsonProperty("CardHolderRelation", Order = 7)]
        internal string CardHolderRelation { get; set; } = "SELF";
    }

    public class SubmitRequestOrderPromotionLine
    {
        [JsonProperty("RuleID", Order = 1)]
        public string RuleID { get; set; }

        /// <summary>
        /// A.k.a. promotion rule name. The same as <seealso cref="SubmitRequestOrderPromotionLine.RuleName"/>
        /// </summary>
        [JsonProperty("PromotionCode", Order = 2)]
        public string PromotionCode { get; set; }

        [JsonProperty("RuleName", Order = 3)]
        public string RuleName { get; set; }

        /// <summary>
        /// Sku or empty in case of CV
        /// </summary>
        [JsonProperty("PromotionItem", Order = 4)]
        public string PromotionItem { get; set; }

        [JsonProperty("Quantity", Order = 5)]
        public int Quantity { get; set; }

        [JsonProperty("SKU", Order = 6)]
        public string SKU { get; set; }


        /// <summary>
        /// Y/N
        /// </summary>
        [JsonProperty("IsAddedToOrder", Order = 7)]
        public string IsAddedToOrder { get; set; }

        /// <summary>
        /// OPTIONAL / AUTOMATIC
        /// </summary>
        [JsonProperty("RedemptionType", Order = 8)]
        public string RedemptionType { get; set; }

        /// <summary>
        /// A.k.a. ChrAttribute1
        /// The same as RuleName for SKU, 'CASH VOUCHER' for CV
        /// </summary>
        [JsonProperty("ChrAttribute1", Order = 9)]
        public string PromotionRuleName { get; set; }

        /// <summary>
        /// 'CASH VOUCHER' for CV, empty for SKU 
        /// </summary>
        [JsonProperty("ChrAttribute2", Order = 10)]
        public string ChrAttribute2 { get; set; }

        /// <summary>
        /// Empty for SKU, cash voucher receipt number for CV
        /// </summary>
        [JsonProperty("ChrAttribute3", Order = 11)]
        public string ChrAttribute3 { get; set; }

        /// <summary>
        /// Empty for SKU; CASH VOUCHER reward type: information about partial redemption
        /// NOTE: partial redemption currently not supported by Promotion Engine. So, 'N' for CV
        /// </summary>
        [JsonProperty("ChrAttribute7", Order = 12)]
        public string ChrAttribute7 { get; set; }

        /// <summary>
        /// Empty for SKU, Cash voucher amount if the Reward Type is CASH VOUCHER
        /// </summary>
        [JsonProperty("NumAttribute1", Order = 13)]
        public decimal? NumAttribute1 { get; set; }
    }
}
