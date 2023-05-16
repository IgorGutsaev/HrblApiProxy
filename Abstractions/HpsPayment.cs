using Filuet.Hrbl.Ordering.Abstractions.Serializers;

using System;
using System.Text.Json.Serialization;

namespace Filuet.Hrbl.Ordering.Abstractions
{
    public class HpsPaymentPayload
    {
        [JsonPropertyName("Country")]
        [JsonPropertyOrder(order: 1)]
        public string Country { get; internal set; }

        [JsonPropertyName("OrderNumber")]
        [JsonPropertyOrder(order: 2)]
        public string OrderNumber { get; internal set; }

        [JsonPropertyName("ClientRefNum")]
        [JsonPropertyOrder(order: 3)]
        public string ClientRefNum { get; internal set; }

        [JsonPropertyName("DistributorId")]
        [JsonPropertyOrder(order: 5)]
        public string DistributorId { get; internal set; }

        [JsonPropertyName("PayCode")]
        [JsonPropertyOrder(order: 6)]
        public string PayCode { get; internal set; }

        [JsonPropertyName("Currency")]
        [JsonPropertyOrder(order: 8)]
        public string Currency { get; internal set; }

        [JsonPropertyName("Amount")]
        [JsonPropertyOrder(order: 9)]
        public string Amount { get; internal set; }

        [JsonPropertyName("CardHolderName")]
        [JsonPropertyOrder(order: 10)]
        public string CardHolderName { get; internal set; }

        [JsonPropertyName("CreditCardNum")]
        [JsonPropertyOrder(order: 11)]
        public string CreditCardNumTokenized { get; internal set; }

        [JsonPropertyName("ExpiryDate")]
        [JsonPropertyOrder(order: 12)]
       // [JsonConverter(typeof(CustomDateTimeConverter))]
        public DateTime ExpiryDate { get; internal set; }

        [JsonPropertyName("CVV2")]
        [JsonPropertyOrder(order: 13)]
        public string CVV2 { get; internal set; }

        [JsonPropertyName("PayeeID")]
        [JsonPropertyOrder(order: 15)]
        public string PayeeID { get; internal set; }

        [JsonPropertyName("Address1")]
        [JsonPropertyOrder(order: 16)]
        public string Address1 { get; internal set; }

        [JsonPropertyName("City")]
        [JsonPropertyOrder(order: 17)]
        public string City { get; internal set; }

        [JsonPropertyName("PostalCode")]
        [JsonPropertyOrder(order: 18)]
        public string PostalCode { get; internal set; }

        [JsonPropertyName("ProcessingLocation")]
        [JsonPropertyOrder(order: 19)]
        public string ProcessingLocation { get; internal set; }

        [JsonPropertyName("OrderType")]
        [JsonPropertyOrder(order: 21)]
        public string OrderType { get; internal set; }

        [JsonPropertyName("Installments")]
        [JsonPropertyOrder(order: 22)]
        public uint Installments { get; internal set; }

        public static HpsPaymentPayload Create(string orderNumber, string distributorId, string currency, decimal amount,
            string cardHolderName, string cardNumber, int expiryMonth, int expiryYear, string cvv2, string country,
            string processingLocation, string cardType, string payeeId, int installments, string clientRefNum, string address, string city, string postalCode, string orderType)
            => new HpsPaymentPayload {
                Country = country,
                OrderNumber = orderNumber,
                ClientRefNum = clientRefNum,
                DistributorId = distributorId,
                PayCode = cardType,
                Currency = currency,
                Amount = amount.ToString(),
                CardHolderName = cardHolderName,
                CreditCardNumTokenized = cardNumber,
                ExpiryDate = new DateTime(expiryYear < 100 ? 2000 + expiryYear : expiryYear, expiryMonth, 1).AddMonths(1).AddDays(-1).Date,
                CVV2 = cvv2,
                PayeeID = payeeId,
                Address1 = address,
                City = city,
                PostalCode = postalCode,
                ProcessingLocation = processingLocation,
                OrderType = orderType,
                Installments = (uint)installments
            };
    }

    internal class HpsPaymentBody : HpsPaymentPayload
    {
        [JsonPropertyName("Operator")]
        [JsonPropertyOrder(order: 4)]
        public string Operator { get; set; } = "Order Taker"; // is constant for the time being

        [JsonPropertyName("PaymentType")]
        [JsonPropertyOrder(order: 7)]
        public string PaymentType { get; set; } = "SALE"; // is constant for the time being

        [JsonPropertyName("Transactionclass")]
        [JsonPropertyOrder(order: 14)]
        public string Transactionclass => string.IsNullOrEmpty(CVV2) ? "CA" : "FA";

        [JsonPropertyName("ApplicationSource")]
        [JsonPropertyOrder(order: 20)]
        public string ApplicationSource { get; set; } = "ORDERPAY"; // is constant for the time being

        [JsonPropertyName("InstallmentsSpecified")]
        [JsonPropertyOrder(order: 23)]
        public bool InstallmentsSpecified => Installments > 0;

        [JsonPropertyName("AuthMerchantNum")]
        [JsonPropertyOrder(order: 24)]
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

        [JsonPropertyName("SettMerchantNum")]
        [JsonPropertyOrder(order: 25)]
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
    }

    internal class HpsPaymentRequest
    {
        [JsonPropertyName("ServiceConsumer")]
        public string ServiceConsumer { get; internal set; }

        [JsonPropertyName("PaymentRequest")]
        public HpsPaymentBody PaymentRequest { get; internal set; } = new HpsPaymentBody();
    }

    internal class HpsPaymentResponse
    {
        [JsonPropertyName("PaymentResponse")] // Also might be ranamed as Errors
        public HpsPaymentResponseDetails PaymentResponse { get; set; }

        [JsonPropertyName("ErrorDetails")] // Also might be ranamed as Errors
        public CommonErrorList Errors { get; set; }
    }

    internal class HpsPaymentResponseDetails
    {
        [JsonPropertyName("OrderNumber")]
        public string OrderNumber { get; set; }

        [JsonPropertyName("ClientRefNum")]
        public string ClientRefNum { get; set; }

        [JsonPropertyName("CreditCardNum")]
        public string CreditCardNum { get; set; }

        [JsonPropertyName("TransactionID")]
        public string TransactionID { get; set; }

        [JsonPropertyName("AuthSystem")]
        public string AuthSystem { get; set; }

        [JsonPropertyName("ApprovalNum")]
        public string ApprovalNum { get; set; }

        [JsonPropertyName("HPPResponseCd")]
        public string HPPResponseCd { get; set; }

        [JsonPropertyName("HPPResponseMsg")]
        public string HPPResponseMsg { get; set; }

        [JsonPropertyName("AuthSystemResponseCd")]
        public string AuthSystemResponseCd { get; set; }

        [JsonPropertyName("AuthResponseMsg")]
        public string AuthResponseMsg { get; set; }

        [JsonPropertyName("Cvv2ResponseCode")]
        public string Cvv2ResponseCode { get; set; }

        [JsonPropertyName("Cvv2ResponseMsg")]
        public string Cvv2ResponseMsg { get; set; }

        [JsonPropertyName("AvsResponseCode")]
        public string AvsResponseCode { get; set; }

        [JsonPropertyName("AvsResponseMsg")]
        public string AvsResponseMsg { get; set; }

        [JsonPropertyName("ProcessedTime")]
        public DateTime? ProcessedTime { get; set; }

        [JsonPropertyName("MerchantNum")]
        public string MerchantNum { get; set; }

        [JsonPropertyName("KountHash")]
        public string KountHash { get; set; }

        [JsonPropertyName("CardType")]
        public string CardType { get; set; }

        [JsonPropertyName("PaymentMethodId")]
        public string PaymentMethodId { get; set; }
    }
}