using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class SubmitRequest
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonPropertyName("OrderHeaders")]
        public SubmitRequestHeader Header { get; internal set; }

        [JsonPropertyName("OrderLine")]
        public SubmitRequestOrderLine[] Lines { get; internal set; }

        [JsonPropertyName("OrderPayment")]
        public SubmitRequestPayment[] Payment { get; internal set; } // 1 mandatory node is payment while optional are Cash Vouchers

        [JsonPropertyName("OrderNotes")]
        public string OrderNotes { get; internal set; }

        [JsonPropertyName("OrderAddress")]
        public string OrderAddress { get; internal set; }

        [JsonPropertyName("OrderPromotionLine")]
        public SubmitRequestOrderPromotionLine[] OrderPromotionLine { get; internal set; }
    }

    public class SubmitRequestHeader
    {
        // Order details mandatory
        [JsonPropertyName("DistributorId")]
        public string DistributorId { get; set; }

        [JsonPropertyName("CustomerName")]
        public string CustomerName { get; set; }

        [JsonPropertyName("ExternalOrderNumber")]
        public string ExternalOrderNumber { get; set; }

        [JsonPropertyName("TotalDue")]
        public decimal TotalDue { get; set; }

        [JsonPropertyName("OrderMonth")]
        //[JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; set; }

        [JsonPropertyName("SalesChannelCode")]
        public string SalesChannelCode { get; set; }

        [JsonPropertyName("OrderDate")]
       // [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; set; }

        [JsonPropertyName("PricingDate")]
       // [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PricingDate { get; set; }

        [JsonPropertyName("TotalVolume")]
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// A.k.a. OrderCategory
        /// </summary>
        [JsonPropertyName("OrderTypeCode")]
        public string OrderTypeCode { get; set; }

        [JsonPropertyName("TotalAmountPaid")]
        public decimal TotalAmountPaid { get; set; }

        [JsonPropertyName("OrderPaymentStatus")]
        public string OrderPaymentStatus { get; set; }

        [JsonPropertyName("OrderDiscount")]
        public decimal OrderDiscountPercent { get; set; } // DiscountPercent from OrderPriceHeader

        // Location payload mandatory (oracle settings basically)
        [JsonPropertyName("CountryCode")]
        public string CountryCode { get; set; }

        [JsonPropertyName("WareHouseCode")]
        public string WareHouseCode { get; set; }

        [JsonPropertyName("ProcessingLocation")]
        public string ProcessingLocation { get; set; }

        /// <summary>
        /// A.k.a. freight code
        /// </summary>
        [JsonPropertyName("ShippingMethodCode")]
        public string ShippingMethodCode { get; set; }

        [JsonPropertyName("City")]
        public string City { get; set; }

        [JsonPropertyName("InvShipFlag")]
        public string InvShipFlag { get; set; }

        [JsonPropertyName("OrgId")]
        [JsonConverter(typeof(StringIntConverter))]
        public int OrgId { get; set; }

        [JsonPropertyName("OrderTypeId")]
        [JsonConverter(typeof(StringIntConverter))]
        public int OrderTypeId { get; set; }

        [JsonPropertyName("PostalCode")]
        public string PostalCode { get; set; }

        [JsonPropertyName("Province")]
        public string Province { get; set; }

        [JsonPropertyName("Address1")]
        public string Address1 { get; set; }

        [JsonPropertyName("Address2")]
        public string Address2 { get; set; }

        [JsonPropertyName("State")]
        public string State { get; set; }

        // Non-mandatory
        [JsonPropertyName("ShippingInstructions")]
        public string ShippingInstructions { get; set; }

        [JsonPropertyName("PickupName")]
        public string PickupName { get; set; } // = string.Empty;

        [JsonPropertyName("TaxAmount")]
        public decimal TaxAmount { get; set; } // Total tax amount

        //[JsonPropertyName("FreightCharges")]
        //[JsonConverter(typeof(StringDecimalConverter))]
        //public decimal FreightCharges { get; set; }

        [JsonPropertyName("DiscountAmount")]
        public decimal DiscountAmount { get; set; } = 0m;

        [JsonPropertyName("OrderConfirmEmail")]
        public string OrderConfirmEmail { get; set; }

        [JsonPropertyName("WillCallFlag")]
        public string WillCallFlag { get; set; } = "N";

        [JsonPropertyName("OrderPurpose")]
        public string OrderPurpose { get; set; } // = string.Empty;

        [JsonPropertyName("SlidingDiscount")]
        public decimal SlidingDiscount { get; set; } = 0m;           // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonPropertyName("OrderSource")]
        public string OrderSource { get; set; }// =  "KIOSK"   хардкод (переделать на знач по-умолчанию)

        [JsonPropertyName("Balance")]
        public decimal Balance { get; set; } = 0m;             // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonPropertyName("TotalRetailPrice")]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }         // спорный момент, скорее не нужен	   = isAALatviaStub ? 0 : 99,    bool isAALatviaStub = string.Equals(salesChannelCode, "AUTOATTENDANT", StringComparison.InvariantCultureIgnoreCase);        

        [JsonPropertyName("ChrAttribute4")]
        public string ChrAttribute4 { get; set; }  //(isPudo ? postamatCode : null)  bool isPudo = freightCode.Equals("BLD") || freightCode.Equals("BLO");

        [JsonPropertyName("ChrAttribute3")]
        public string ChrAttribute3 { get; set; } = "N";          // isPudo ?"Y":"N"

        [JsonPropertyName("ChrAttribute6")]
        public string ChrAttribute6 { get; set; } = "QR";            // QR хардкод (для Балтики как минимум)

        [JsonPropertyName("ChrAttribute5")]
        public string ChrAttribute5 { get; set; }          //(isPudo ? phone : null)

        [JsonPropertyName("Phone")]
        public string Phone { get; set; }  // null по ум 

        [JsonPropertyName("Notes")]
        public string Notes { get; set; }            //string     не mandatory (null по-умолчанию)

        [JsonPropertyName("SMSNumber")]
        public string SMSNumber { get; set; }                // null string

        [JsonPropertyName("SMSAction")]
        public string SMSAction { get; set; }// = "ORDER COMPLETION";       //"ORDER COMPLETION" по-умолчанию

        [JsonPropertyName("SMSRole")]
        public string SMSRole { get; set; }// = "DS";           //      "DS" по-умолчанию

        [JsonPropertyName("OrderSubType")]
        public string OrderSubType { get; set; } // = string.Empty     // "" по-умолчанию
    }

    public class SubmitRequestOrderLine
    {
        [JsonPropertyName("SKU")]
        [JsonPropertyOrder(order: 1)]
        public string Sku { get; set; }

        [JsonPropertyName("Quantity")]
        [JsonPropertyOrder(order: 2)]
        // [JsonConverter(typeof(StringIntConverter))]
        public decimal Quantity { get; set; }

        [JsonPropertyName("LineAmount")]
        [JsonPropertyOrder(order: 3)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal Amount { get; set; }

        [JsonPropertyName("UnitVolume")]
        [JsonPropertyOrder(order: 4)]
        // [JsonConverter(typeof(StringDecimalConverter))]
        public decimal UnitVolume { get; set; }

        [JsonPropertyName("EarnBase")]
        [JsonPropertyOrder(order: 5)]
        // [JsonConverter(typeof(StringDecimalConverter))]
        public decimal EarnBase { get; set; }

        [JsonPropertyName("TotalRetailPrice")]
        [JsonPropertyOrder(order: 6)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }

        [JsonPropertyName("TotalDiscountedPrice")]
        [JsonPropertyOrder(order: 7)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalDiscountedPrice { get; set; }

        [JsonPropertyName("ProductType")]
        [JsonPropertyOrder(order: 8)]
        public string ProductType { get; set; } = "P";
    }

    public class SubmitRequestPayment
    {
        [JsonPropertyName("PaymentMethodName")]
        [JsonPropertyOrder(order: 1)]
        public string PaymentMethodName { get; set; }

        [JsonPropertyName("PaymentStatus")]
        [JsonPropertyOrder(order: 2)]
        public string PaymentStatus { get; set; }

        [JsonPropertyName("PaymentMethodId")]
        [JsonPropertyOrder(order: 3)]
        public string PaymentMethodId { get; set; }

        [JsonPropertyName("PaymentAmount")]
        [JsonPropertyOrder(order: 4)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentAmount { get; set; }

        [JsonPropertyName("PaymentDate")]
        [JsonPropertyOrder(order: 5)]
        //[JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime Date { get; set; } // nullable

        [JsonPropertyName("Paycode")]
        [JsonPropertyOrder(order: 6)]
        public string Paycode { get; set; } // CARD CASH

        [JsonPropertyName("PaymentType")]
        [JsonPropertyOrder(order: 7)]
        public string PaymentType { get; set; }

        [JsonPropertyName("CurrencyCode")]
        [JsonPropertyOrder(order: 8)]
        public string CurrencyCode { get; set; }

        [JsonPropertyName("AppliedDate")]
        [JsonPropertyOrder(order: 9)]
        //[JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime AppliedDate { get; set; }

        [JsonPropertyName("ApprovalNumber")]
        [JsonPropertyOrder(order: 10)]
        public string ApprovalNumber { get; set; }        
        
        [JsonPropertyName("CheckWireNumber")]
        [JsonPropertyOrder(order: 11)]
        public string CheckWireNumber { get; set; }

        [JsonPropertyName("PaymentReceived")]
        [JsonPropertyOrder(order: 12)]
        //[JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentReceived { get; set; }

        [JsonPropertyName("CreditCard")]
        [JsonPropertyOrder(order: 13)]
        public OrderSubmitPaymentCreditCard CreditCard { get; set; } = new OrderSubmitPaymentCreditCard();

        [JsonPropertyName("AuthorizationType")]
        [JsonPropertyOrder(order: 14)]
        internal string AuthorizationType { get; set; } = "ONLINE";

        [JsonPropertyName("VoidFlag")]
        [JsonPropertyOrder(order: 15)]
        internal string VoidFlag { get; set; } = "N";

        [JsonPropertyName("ClientRefNumber")]
        [JsonPropertyOrder(order: 16)]
        public string ClientRefNumber { get; set; }
    }

    public class OrderSubmitPaymentCreditCard
    {
        [JsonPropertyName("CardNumber")]
        [JsonPropertyOrder(order: 1)]
        public string CardNumber { get; set; }

        [JsonPropertyName("TrxApprovalNumber")]
        [JsonPropertyOrder(order: 2)]
        public string TrxApprovalNumber { get; set; }

        [JsonPropertyName("CardType")]
        [JsonPropertyOrder(order: 3)]
        public string CardType { get; set; }

        [JsonPropertyName("CardExpiryDate")]
        [JsonPropertyOrder(order: 4)]
        //[JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime CardExpiryDate { get; set; }

        [JsonPropertyName("CardHolderName")]
        [JsonPropertyOrder(order: 5)]
        public string CardHolderName { get; set; }

        [JsonPropertyName("CardValidationType")]
        [JsonPropertyOrder(order: 6)]
        internal string CardValidationType { get; set; } = "ONLINE";

        [JsonPropertyName("CardHolderRelation")]
        [JsonPropertyOrder(order: 7)]
        internal string CardHolderRelation { get; set; } = "SELF";
    }

    public class SubmitRequestOrderPromotionLine
    {
        [JsonPropertyName("RuleID")]
        [JsonPropertyOrder(order: 1)]
        public string RuleID { get; set; }

        /// <summary>
        /// A.k.a. promotion rule name. The same as <seealso cref="SubmitRequestOrderPromotionLine.RuleName"/>
        /// </summary>
        [JsonPropertyName("PromotionCode")]
        [JsonPropertyOrder(order: 2)]
        public string PromotionCode { get; set; }

        [JsonPropertyName("RuleName")]
        [JsonPropertyOrder(order: 3)]
        public string RuleName { get; set; }

        /// <summary>
        /// Sku or empty in case of CV
        /// </summary>
        [JsonPropertyName("PromotionItem")]
        [JsonPropertyOrder(order: 4)]
        public string PromotionItem { get; set; }

        [JsonPropertyName("Quantity")]
        [JsonPropertyOrder(order: 5)]
        public int Quantity { get; set; }

        [JsonPropertyName("SKU")]
        [JsonPropertyOrder(order: 6)]
        public string SKU { get; set; }


        /// <summary>
        /// Y/N
        /// </summary>
        [JsonPropertyName("IsAddedToOrder")]
        [JsonPropertyOrder(order: 7)]
        public string IsAddedToOrder { get; set; }

        /// <summary>
        /// OPTIONAL / AUTOMATIC
        /// </summary>
        [JsonPropertyName("RedemptionType")]
        [JsonPropertyOrder(order: 8)]
        public string RedemptionType { get; set; }

        /// <summary>
        /// A.k.a. ChrAttribute1
        /// The same as RuleName for SKU, 'CASH VOUCHER' for CV
        /// </summary>
        [JsonPropertyName("ChrAttribute1")]
        [JsonPropertyOrder(order: 9)]
        public string PromotionRuleName { get; set; }

        /// <summary>
        /// 'CASH VOUCHER' for CV, empty for SKU 
        /// </summary>
        [JsonPropertyName("ChrAttribute2")]
        [JsonPropertyOrder(order: 10)]
        public string ChrAttribute2 { get; set; }

        /// <summary>
        /// Empty for SKU, cash voucher receipt number for CV
        /// </summary>
        [JsonPropertyName("ChrAttribute3")]
        [JsonPropertyOrder(order: 11)]
        public string ChrAttribute3 { get; set; }

        /// <summary>
        /// Empty for SKU; CASH VOUCHER reward type: information about partial redemption
        /// NOTE: partial redemption currently not supported by Promotion Engine. So, 'N' for CV
        /// </summary>
        [JsonPropertyName("ChrAttribute7")]
        [JsonPropertyOrder(order: 12)]
        public string ChrAttribute7 { get; set; }

        /// <summary>
        /// Empty for SKU, Cash voucher amount if the Reward Type is CASH VOUCHER
        /// </summary>
        [JsonPropertyName("NumAttribute1")]
        [JsonPropertyOrder(order: 13)]
        public decimal? NumAttribute1 { get; set; }
    }
}