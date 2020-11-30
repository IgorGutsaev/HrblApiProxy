using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    internal class SubmitRequest
    {
        [JsonProperty("ServiceConsumer")]
        internal string ServiceConsumer { get; set; }

        [JsonProperty("OrderHeaders")]
        internal SubmitRequestHeader Header { get; set; }

        [JsonProperty("OrderLine")]
        internal SubmitRequestOrderLine[] Lines { get; set; }

        [JsonProperty("OrderPayment")]
        internal SubmitRequestPayment Payment { get; set; }
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
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalDue { get; set; }

        [JsonProperty("OrderMonth")]
        [JsonConverter(typeof(OrderMonthSelectDateTimeConverter))]
        public DateTime OrderMonth { get; set; }

        [JsonProperty("SalesChannelCode")]
        public string SalesChannelCode { get; set; }

        [JsonProperty("ReferenceNumber")]
        public string ReferenceNumber { get; set; }  // e.g. Kiosk UID

        [JsonProperty("OrderDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime OrderDate { get; set; }

        [JsonProperty("PricingDate")]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime PricingDate { get; set; }

        [JsonProperty("TotalVolume")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalVolume { get; set; }

        /// <summary>
        /// A.k.a. OrderCategory
        /// </summary>
        [JsonProperty("OrderTypeCode")]
        public string OrderTypeCode { get; set; }

        [JsonProperty("TotalAmountPaid")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalAmountPaid { get; set; }

        [JsonProperty("OrderPaymentStatus")]
        public string OrderPaymentStatus { get; set; }

        /// <summary>
        /// DiscountPercent probably
        /// </summary>
        [JsonProperty("OrderDiscount")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal OrderDiscount { get; set; } // DiscountPercent from OrderPriceHeader

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
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TaxAmount { get; set; } // ???

        [JsonProperty("DiscountAmount")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal DiscountAmount { get; set; } = 0m;

        [JsonProperty("OrderConfirmEmail")]
        public string OrderConfirmEmail { get; set; }

        [JsonProperty("WillCallFlag")]
        public string WillCallFlag { get; set; } = "N";

        [JsonProperty("OrderPurpose")]
        public string OrderPurpose { get; set; } // = string.Empty;

        [JsonProperty("SlidingDiscount")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal SlidingDiscount { get; set; } = 0m;           // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonProperty("OrderSource")]
        public string OrderSource { get; set; }// =  "KIOSK"   хардкод (переделать на знач по-умолчанию)

        [JsonProperty("Balance")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Balance { get; set; } = 0m;             // = 0 по-умолчанию, в агрументы передавать не нужно

        [JsonProperty("TotalRetailPrice")]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }         // спорный момент, скорее не нужен	   = isAALatviaStub ? 0 : 99,    bool isAALatviaStub = string.Equals(salesChannelCode, "AUTOATTENDANT", StringComparison.InvariantCultureIgnoreCase);        

        [JsonProperty("ChrAttribute4")]
        public string ChrAttribute4 { get; set; }  //(isPudo ? postamatCode : null)  bool isPudo = freightCode.Equals("BLD") || freightCode.Equals("BLO");

        [JsonProperty("ChrAttribute3")]
        public string ChrAttribute3 { get; set; }           // isPudo ?"Y":"N"

        [JsonProperty("ChrAttribute6")]
        public string ChrAttribute6 { get; set; } // = "QR"              // QR хардкод (для Балтики как минимум)

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
        [JsonConverter(typeof(StringIntConverter))]
        public int Quantity { get; set; }

        [JsonProperty("LineAmount", Order = 3)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal Amount { get; set; }

        [JsonProperty("UnitVolume", Order = 4)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal UnitVolume { get; set; }

        [JsonProperty("EarnBase", Order = 5)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal EarnBase { get; set; }

        [JsonProperty("UnitEarnBase", Order = 6)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal UnitEarnBase { get; set; } // Возможно TotalEarnbase в soap

        [JsonProperty("TotalRetailPrice", Order = 7)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalRetailPrice { get; set; }

        [JsonProperty("TotalDiscountedPrice", Order = 8)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal TotalDiscountedPrice { get; set; }
    }

    internal class SubmitRequestPayment
    {
        [JsonProperty("PaymentMethodName", Order = 1)]
        public string PaymentMethodName { get; set; }

        [JsonProperty("PaymentMethodId", Order = 2)]
        [JsonConverter(typeof(StringIntConverter))]
        public int PaymentMethodId { get; set; }

        [JsonProperty("PaymentAmount", Order = 3)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentAmount { get; set; }

        [JsonProperty("PaymentDate", Order = 4)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime Date { get; set; } // nullable

        [JsonProperty("Paycode", Order = 5)]
        public string Paycode { get; set; } // CARD CASH

        [JsonProperty("PaymentType", Order = 6)]
        public string PaymentType { get; set; }

        [JsonProperty("CurrencyCode", Order = 7)]
        public string CurrencyCode { get; set; }

        [JsonProperty("AppliedDate", Order = 8)]
        [JsonConverter(typeof(StandardDateTimeConverter))]
        public DateTime AppliedDate { get; set; }

        [JsonProperty("ApprovalNumber", Order = 9)]
        public string ApprovalNumber { get; set; }

        [JsonProperty("PaymentReceived", Order = 10)]
        [JsonConverter(typeof(StringDecimalConverter))]
        public decimal PaymentReceived { get; set; }

        [JsonProperty("CreditCard", Order = 11)]
        public OrderSubmitPaymentCreditCard CreditCard { get; set; }
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
    }
}
