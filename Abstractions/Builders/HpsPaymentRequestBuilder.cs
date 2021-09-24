using System;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Builders
{
    internal class HpsPaymentRequestBuilder
    {
        private HpsPaymentRequest _request = new HpsPaymentRequest();

        internal HpsPaymentRequestBuilder AddServiceConsumer(string consumer)
        {
            if (string.IsNullOrWhiteSpace(consumer))
                throw new ArgumentException("Service consumer is mandatory");

            _request.ServiceConsumer = consumer;

            return this;
        }

        internal HpsPaymentRequestBuilder AddPayload(HpsPaymentPayload payload)
        {
            #region Validation
            StringBuilder issues = new StringBuilder();

            if (string.IsNullOrWhiteSpace(payload.Country) || payload.Country.Trim().Length != 2)
                issues.AppendLine($"Invalid country '{payload.Country}'");

            if (string.IsNullOrWhiteSpace(payload.OrderNumber))
                issues.AppendLine("Order number is mandatory");

            if (string.IsNullOrWhiteSpace(payload.DistributorId))
                issues.AppendLine("Distributor id is mandatory");

            if (string.IsNullOrWhiteSpace(payload.Currency))
                issues.AppendLine("Currency is mandatory");

            decimal amount = 0m;
            if (!decimal.TryParse(payload.Amount.Replace(",", "."), out amount))
                issues.AppendLine("Invalid order total due");
            else if (amount <= 0)
                issues.AppendLine("Amount must be positive");

            if (string.IsNullOrWhiteSpace(payload.CreditCardNumTokenized))
                issues.AppendLine("Card number is mandatory");

            if (string.IsNullOrWhiteSpace(payload.CVV2))
                issues.AppendLine("CVV2 is mandatory");

            if (string.IsNullOrWhiteSpace(payload.ProcessingLocation))
                issues.AppendLine("Processing location is mandatory");

            if ((payload.ExpiryDate.Month < DateTime.Now.Month 
                && payload.ExpiryDate.Year == DateTime.Now.Year) || payload.ExpiryDate.Year < DateTime.Now.Year)
                issues.AppendLine("Invalid card expiration date");

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());
            #endregion

            _request.PaymentRequest.Country = payload.Country;
            _request.PaymentRequest.OrderNumber = payload.OrderNumber;
            _request.PaymentRequest.ClientRefNum = payload.ClientRefNum;
            _request.PaymentRequest.DistributorId = payload.DistributorId;
            _request.PaymentRequest.PayCode = payload.PayCode;
            _request.PaymentRequest.Currency = payload.Currency;
            _request.PaymentRequest.Amount = payload.Amount;
            _request.PaymentRequest.CardHolderName = payload.CardHolderName;
            _request.PaymentRequest.CreditCardNumTokenized = payload.CreditCardNumTokenized;
            _request.PaymentRequest.ExpiryDate = payload.ExpiryDate;
            _request.PaymentRequest.CVV2 = payload.CVV2;
            _request.PaymentRequest.PayeeID = payload.PayeeID;
            _request.PaymentRequest.Address1 = payload.Address1;
            _request.PaymentRequest.City = payload.City;
            _request.PaymentRequest.PostalCode = payload.PostalCode;
            _request.PaymentRequest.ProcessingLocation = payload.ProcessingLocation;
            _request.PaymentRequest.OrderType = payload.OrderType;
            _request.PaymentRequest.Installments = payload.Installments;

            return this;
        }

        internal HpsPaymentRequest Build() => _request;
    }
}
