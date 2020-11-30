using Filuet.Hrbl.Ordering.Common;
using System;
using System.Linq;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Builders
{
    public class SubmitRequestBuilder
    {
        private SubmitRequest _request = new SubmitRequest();

        internal SubmitRequestBuilder AddServiceConsumer(string consumer)
        {
            if (string.IsNullOrWhiteSpace(consumer))
                throw new ArgumentException("Service consumer is mandatory");

            _request.ServiceConsumer = consumer;

            return this;
        }

        public SubmitRequestBuilder AddHeader(Action<SubmitRequestHeader> setupHeader)
        {
            SubmitRequestHeader header = setupHeader?.CreateTargetAndInvoke();

            #region Validation
            StringBuilder issues = new StringBuilder();

            if (string.IsNullOrWhiteSpace(header.DistributorId))
                issues.AppendLine($"Distributor id is mandatory");

            if (string.IsNullOrWhiteSpace(header.CustomerName))
                issues.AppendLine($"Customer name is mandatory");

            if (string.IsNullOrWhiteSpace(header.ExternalOrderNumber))
                issues.AppendLine($"Order number is mandatory");

            if (header.TotalDue <= 0m)
                issues.AppendLine($"Total due must be positive");

            if (header.OrderMonth == DateTime.MinValue)
                issues.AppendLine($"Invalid order month");

            if (string.IsNullOrWhiteSpace(header.SalesChannelCode))
                issues.AppendLine($"Sales channel code is mandatory");

            if (header.OrderDate == DateTime.MinValue)
                issues.AppendLine($"Invalid order date");

            if (header.PricingDate == DateTime.MinValue)
                issues.AppendLine($"Invalid pricing date");

            if (header.TotalVolume < 0)
                issues.AppendLine($"Total volume must be non-negative");

            if (string.IsNullOrWhiteSpace(header.OrderTypeCode))
                issues.AppendLine($"Order type code is mandatory");

            if (header.TotalAmountPaid < 0m)
                issues.AppendLine($"Total amount paid must be non-negative");

            if (string.IsNullOrWhiteSpace(header.OrderPaymentStatus))
                issues.AppendLine($"Order payment status is mandatory");

            if (header.OrderDiscountPercent < 0m)
                issues.AppendLine($"Order discount must be non-negative");

            if (string.IsNullOrWhiteSpace(header.CountryCode))
                issues.AppendLine($"Country code is mandatory");

            if (string.IsNullOrWhiteSpace(header.WareHouseCode))
                issues.AppendLine($"WareHouse code is mandatory");

            if (string.IsNullOrWhiteSpace(header.ProcessingLocation))
                issues.AppendLine($"Processing location is mandatory");

            if (string.IsNullOrWhiteSpace(header.ShippingMethodCode))
                issues.AppendLine($"Shipping method code is mandatory");

            if (string.IsNullOrWhiteSpace(header.City))
                issues.AppendLine($"City is mandatory");

            //if (header.FreightCharges < 0m)
            //    issues.AppendLine($"Freight charge must be non-negative");

            if (string.IsNullOrWhiteSpace(header.InvShipFlag))
                issues.AppendLine($"InvShipFlag is mandatory");

            if (header.OrgId <= 0)
                issues.AppendLine($"OrgId must be positive");

            if (string.IsNullOrWhiteSpace(header.PostalCode))
                issues.AppendLine($"Postal code is mandatory");

            if (string.IsNullOrWhiteSpace(header.Address1))
                issues.AppendLine($" is mandatory");

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());
            #endregion

            _request.Header = header;

            return this;
        }

        public SubmitRequestBuilder AddPayment(Action<SubmitRequestPayment> setupPayment)
        {
            SubmitRequestPayment payment = setupPayment?.CreateTargetAndInvoke();

            #region Validation
            StringBuilder issues = new StringBuilder();

            if (string.IsNullOrWhiteSpace(payment.PaymentMethodName))
                issues.AppendLine($"Payment method name is mandatory");

            // payment.PaymentMethodId // is optional

            if (payment.PaymentAmount <= 0m)
                issues.AppendLine($"Payment amount must be non-negative");

            if (payment.Date == DateTime.MinValue)
                issues.AppendLine($"Payment date must be specified");

            if (string.IsNullOrWhiteSpace(payment.Paycode))
                issues.AppendLine($"Paycode is mandatory");

            if (string.IsNullOrWhiteSpace(payment.PaymentType))
                issues.AppendLine($"Payment type is mandatory");

            if (string.IsNullOrWhiteSpace(payment.CurrencyCode))
                issues.AppendLine($"Currency code is mandatory");

            if (payment.AppliedDate == DateTime.MinValue)
                issues.AppendLine($"Applied date must be specified");

            // ApprovalNumber is optional (e.g. cash payment)

            if (payment.PaymentReceived <= 0m)
                issues.AppendLine($"Payment received must be non-negative");


            if ((payment.PaymentMethodName ?? string.Empty).ToLower().Contains("card"))
            {
                if (payment.CreditCard == null)
                    issues.AppendLine($"Credit card block is mandatory");
                else
                {
                    if (string.IsNullOrWhiteSpace(payment.CreditCard.CardNumber))
                        issues.AppendLine($"Credit card number is mandatory");

                    if (string.IsNullOrWhiteSpace(payment.CreditCard.CardHolderName))
                        issues.AppendLine($"Card holder name is mandatory");

                    if (string.IsNullOrWhiteSpace(payment.CreditCard.TrxApprovalNumber))
                        issues.AppendLine($"Transaction approval number is mandatory");

                    if (string.IsNullOrWhiteSpace(payment.CreditCard.CardType))
                        issues.AppendLine($"Card type is mandatory");

                    if (payment.CreditCard.CardExpiryDate <= DateTime.Now.AddDays(1))
                        issues.AppendLine($"Invcalid card expiration date");
                }
            }
            else payment.CreditCard = null;

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());
            #endregion

            _request.Payment = payment;

            return this;
        }

        public SubmitRequestBuilder AddItems(Func<SubmitRequestOrderLine[]> setupPayment)
        {
            SubmitRequestOrderLine[] lines = setupPayment?.Invoke();

            if (!lines.Any())
                throw new ArgumentException("The order is empty (no order lines)");

            StringBuilder issues = new StringBuilder();

            int index = 0;
            foreach (SubmitRequestOrderLine l in lines)
            {
                index++;

                if (string.IsNullOrWhiteSpace(l.Sku))
                    issues.AppendLine($"[line {index}] Sku is mandatory");

                if (l.Quantity <= 0)
                    issues.AppendLine($"[line {index}] Quantity must be positive");

                if (l.Amount <= 0m)
                    issues.AppendLine($"[line {index}] Amount must be non-negative");

                if (l.UnitVolume <= 0m)
                    issues.AppendLine($"[line {index}] Amount must be non-negative");
            }

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());

            _request.Lines = lines;

            return this;
        }

        internal SubmitRequest Build() => _request;
    }
}
