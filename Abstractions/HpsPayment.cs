using Filuet.Hrbl.Ordering.Abstractions.Serializers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class HpsPaymentPayload
    {
        [JsonProperty("Country", Order = 1)]
        public string Country { get; internal set; }

        [JsonProperty("OrderNumber", Order = 2)]
        public string OrderNumber { get; internal set; }

        [JsonProperty("ClientRefNum", Order = 3)]
        public string ClientRefNum { get; internal set; }

        [JsonProperty("DistributorId", Order = 5)]
        public string DistributorId { get; internal set; }

        [JsonProperty("PayCode", Order = 6)]
        public string PayCode { get; internal set; }

        [JsonProperty("Currency", Order = 8)]
        public string Currency { get; internal set; }

        [JsonProperty("Amount", Order = 9)]
        public string Amount { get; internal set; }

        [JsonProperty("CardHolderName", Order = 10)]
        public string CardHolderName { get; internal set; }

        [JsonProperty("CreditCardNum", Order = 11)]
        public string CreditCardNumTokenized { get; internal set; }

        [JsonProperty("ExpiryDate", Order = 12)]
        [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExpiryDate { get; internal set; }

        [JsonProperty("CVV2", Order = 13)]
        public string CVV2 { get; internal set; }

        [JsonProperty("PayeeID", Order = 15)]
        public string PayeeID { get; internal set; }

        [JsonProperty("Address1", Order = 16)]
        public string Address1 { get; internal set; }

        [JsonProperty("City", Order = 17)]
        public string City { get; internal set; }

        [JsonProperty("PostalCode", Order = 18)]
        public string PostalCode { get; internal set; }

        [JsonProperty("ProcessingLocation", Order = 19)]
        public string ProcessingLocation { get; internal set; }

        [JsonProperty("OrderType", Order = 21)]
        public string OrderType { get; internal set; }

        [JsonProperty("Installments", Order = 22)]
        public uint Installments { get; internal set; }
    }

    internal class HpsPaymentBody : HpsPaymentPayload
    {
        [JsonProperty("Operator", Order = 4)]
        public string Operator { get; private set; } = "Order Taker"; // is constant for the time being

        [JsonProperty("PaymentType", Order = 7)]
        public string PaymentType { get; private set; } = "SALE"; // is constant for the time being

        [JsonProperty("Transactionclass", Order = 14)]
        public string Transactionclass => string.IsNullOrEmpty(CVV2) ? "CA" : "FA";

        [JsonProperty("ApplicationSource", Order = 20)]
        public string ApplicationSource { get; private set; } = "ORDERPAY"; // is constant for the time being

        [JsonProperty("AuthMerchantNum", Order = 24)]
        public string AuthMerchantNum
        {
            get
            {
                switch (Country.ToUpper())
                {
                    case "KS":
                        return "DPT0002801";
                    case "FR":
                        return "76851442";
                    default:
                        return null;
                }
            }
        }

        [JsonProperty("SettMerchantNum", Order = 25)]
        public string SettMerchantNum
        {
            get
            {
                switch (Country.ToUpper())
                {
                    case "KS":
                        return "704430408";
                    case "FR":
                        return "405598";
                    default:
                        return null;
                }
            }
        }

        [JsonProperty("InstallmentsSpecified", Order = 23)]
        public bool InstallmentsSpecified => Installments > 0;
    }

    internal class HpsPaymentRequest
    {
        [JsonProperty("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonProperty("PaymentRequest")]
        public HpsPaymentBody PaymentRequest { get; internal set; } = new HpsPaymentBody();
    }

    internal class HpsPaymentResponse
    {
        [JsonProperty("PaymentResponse")] // Also might be ranamed as Errors
        public HpsPaymentResponseDetails PaymentResponse { get; private set; }

        [JsonProperty("ErrorDetails")] // Also might be ranamed as Errors
        public CommonErrorList Errors { get; private set; }
    }

    internal class HpsPaymentResponseDetails
    {
        [JsonProperty("OrderNumber")]
        public string OrderNumber { get; private set; }

        [JsonProperty("ClientRefNum")]
        public string ClientRefNum { get; private set; }

        [JsonProperty("CreditCardNum")]
        public string CreditCardNum { get; private set; }

        [JsonProperty("TransactionID")]
        public string TransactionID { get; private set; }

        [JsonProperty("AuthSystem")]
        public string AuthSystem { get; private set; }

        [JsonProperty("ApprovalNum")]
        public string ApprovalNum { get; private set; }

        [JsonProperty("HPPResponseCd")]
        public string HPPResponseCd { get; private set; }

        [JsonProperty("HPPResponseMsg")]
        public string HPPResponseMsg { get; private set; }

        [JsonProperty("AuthSystemResponseCd")]
        public string AuthSystemResponseCd { get; private set; }

        [JsonProperty("AuthResponseMsg")]
        public string AuthResponseMsg { get; private set; }

        [JsonProperty("Cvv2ResponseCode")]
        public string Cvv2ResponseCode { get; private set; }

        [JsonProperty("Cvv2ResponseMsg")]
        public string Cvv2ResponseMsg { get; private set; }

        [JsonProperty("AvsResponseCode")]
        public string AvsResponseCode { get; private set; }

        [JsonProperty("AvsResponseMsg")]
        public string AvsResponseMsg { get; private set; }

        [JsonProperty("ProcessedTime")]
        public string ProcessedTime { get; private set; }

        [JsonProperty("MerchantNum")]
        public string MerchantNum { get; private set; }

        [JsonProperty("KountHash")]
        public string KountHash { get; private set; }

        [JsonProperty("CardType")]
        public string CardType { get; private set; }

        [JsonProperty("PaymentMethodId")]
        public string PaymentMethodId { get; private set; }
    }
}
