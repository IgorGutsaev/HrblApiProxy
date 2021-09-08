using Filuet.Hrbl.Ordering.Common;
using System;
using System.Linq;
using System.Text;

namespace Filuet.Hrbl.Ordering.Abstractions.Builders
{
    public class PricingRequestBuilder
    {
        private PricingRequest _request = new PricingRequest();

        internal PricingRequestBuilder AddServiceConsumer(string consumer)
        {
            if (string.IsNullOrWhiteSpace(consumer))
                throw new ArgumentException("Service consumer is mandatory");

            _request.ServiceConsumer = consumer;

            return this;
        }

        public PricingRequestBuilder AddHeader(Action<PricingRequestHeader> setupHeader)
        {
            PricingRequestHeader header = setupHeader?.CreateTargetAndInvoke();

            #region Validation
            StringBuilder issues = new StringBuilder();

            if (string.IsNullOrWhiteSpace(header.DistributorId))
                issues.AppendLine($"Distributor id is mandatory");

            if (string.IsNullOrWhiteSpace(header.OrderSource))
                issues.AppendLine($"Order source id is mandatory");

            if (header.ExternalOrderNumber != null && header.ExternalOrderNumber.Trim() == string.Empty)
                issues.AppendLine($"Invalid external order number. It might be null (to get a brand new order number from fusion) or specified (from previous pricing request)");

            if (string.IsNullOrWhiteSpace(header.CurrencyCode))
                issues.AppendLine($"Currency is mandatory"); // fusion doesn't compute currency of order

            if (header.OrderMonth == DateTime.MinValue)
                issues.AppendLine($"Invalid order month");

            if (header.OrderDate == DateTime.MinValue)
                issues.AppendLine($"Invalid order date");

            if (header.PriceDate == DateTime.MinValue)
                issues.AppendLine($"Invalid price date");

            if (string.IsNullOrWhiteSpace(header.OrderType))
                issues.AppendLine($"Order type is mandatory");

            if (string.IsNullOrWhiteSpace(header.OrderCategory))
                issues.AppendLine($"Order category is mandatory");

            if (string.IsNullOrWhiteSpace(header.CountryCode))
                issues.AppendLine($"Country code is mandatory");

            if (string.IsNullOrWhiteSpace(header.Warehouse))
                issues.AppendLine($"WareHouse code is mandatory");

            if (string.IsNullOrWhiteSpace(header.ProcessingLocation))
                issues.AppendLine($"Processing location is mandatory");

            if (string.IsNullOrWhiteSpace(header.FreightCode))
                issues.AppendLine($"Freight code is mandatory");

            if (string.IsNullOrWhiteSpace(header.City))
                issues.AppendLine($"City is mandatory");

            if (string.IsNullOrWhiteSpace(header.PostalCode) && header.OrderSource == "AAKIOSK")
                issues.AppendLine($"Postal code is mandatory");

            if (string.IsNullOrWhiteSpace(header.Address1) && header.OrderSource == "AAKIOSK")
                issues.AppendLine($"Address is mandatory");

            if (string.IsNullOrWhiteSpace(header.ProcessingLocation))
                issues.AppendLine($"Processing location is mandatory");

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());
            #endregion

            _request.Header = header;

            return this;
        }

        public PricingRequestBuilder AddItems(Func<PricingRequestLine[]> setupPayment)
        {
            PricingRequestLine[] lines = setupPayment?.Invoke();

            if (!lines.Any())
                throw new ArgumentException("The shopping cart is empty (no order lines)");

            StringBuilder issues = new StringBuilder();

            int index = 0;
            foreach (PricingRequestLine l in lines)
            {
                index++;

                if (string.IsNullOrWhiteSpace(l.Sku))
                    issues.AppendLine($"[line {index}] Sku is mandatory");

                if (string.IsNullOrWhiteSpace(l.ProcessingLocation))
                    issues.AppendLine($"[line {index}] Processing location is mandatory");

                if (string.IsNullOrWhiteSpace(l.ProductType)) // P by default
                    issues.AppendLine($"[line {index}] Product type is mandatory");

                if (l.Quantity <= 0)
                    issues.AppendLine($"[line {index}] Quantity must be positive");
            }

            if (issues.Length > 0)
                throw new ArgumentException(issues.ToString());

            _request.Lines = lines;

            return this;
        }

        public PricingRequest Build() => _request;
    }
}