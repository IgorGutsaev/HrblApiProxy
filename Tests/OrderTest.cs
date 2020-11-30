using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Common;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class OrderTest : BaseTest
    {
        [Fact]
        public async Task Test_get_Pricing_order()
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate

            // Perform
            string result = await _adapter.GetPriceDetails(new PricingRequest { });

            // Post-validate
            Assert.NotNull(result);
            Assert.True(result.Length == 1);
        }

        /// <summary>
        /// Try to pay with invalid card data (works in the production only for the time being)
        /// </summary>
        /// <returns></returns>
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetHpsPayloadsWithCorruptedNumber), MemberType = typeof(TestDataGenerator))]
        public async Task Test_HPS_payment_with__Corrupted_card_number(
            string country, string processingLocation, string distributorId, string orderNumber, string currency, decimal amount,
            string cardHolderName, string cardNumber, string cardType, DateTime cardExpirationMonth, string cvv2, string payeeId,
            uint installments, string clientRefNum, string address, string city, string postalCode, string orderType)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.Equal(_adapter.Environment, HrblEnvironment.Prod);

            // Perform
            HrblRestApiException ex = await Assert.ThrowsAsync<HrblRestApiException>(async () =>
            await _adapter.HpsPaymentGateway(new HpsPaymentPayload
            {
                Country = country,
                OrderNumber = orderNumber,
                ClientRefNum = clientRefNum,
                DistributorId = distributorId,
                PayCode = cardType,
                Currency = currency,
                Amount = amount.ToString(),
                CardHolderName = cardHolderName,
                CreditCardNumTokenized = cardNumber,
                ExpiryDate = cardExpirationMonth,
                CVV2 = cvv2,
                PayeeID = payeeId,
                Address1 = address,
                City = city,
                PostalCode = postalCode,
                ProcessingLocation = processingLocation,
                OrderType = orderType,
                Installments = installments
            }));

            // Post-validate
            Assert.NotNull(ex);
            Assert.Equal("Invalid card number", ex.Message);
        }

        [Fact]
        public async Task Test_Submit_Order()
        {
            // Prepare
            Action<SubmitRequestBuilder> setupAction = (b) =>
                b.AddHeader(h =>
                {
                    h.DistributorId = "U512310066";
                    h.CustomerName = "ILVA VISTERE";
                    h.SalesChannelCode = "AUTOSTORE";
                    h.ReferenceNumber = "LVRIGAS3";
                    h.WareHouseCode = "LR";
                    h.ProcessingLocation = "LR";
                    h.OrderMonth = new DateTime(2020, 11, 1);
                    h.ShippingMethodCode = "LR";
                    h.CountryCode = "LV";
                    h.PostalCode = "LV-1073";
                    h.Address1 = "Piedrujas street, 7a";
                    h.City = "Riga";
                    h.ExternalOrderNumber = "LRK1336308";
                    h.OrderTypeCode = "RSO";
                    // h.OrderTypeId = ???,
                    h.PricingDate = new DateTime(2020, 11, 27, 8, 31, 44);
                    h.OrderDate = new DateTime(2020, 11, 27, 8, 31, 44);
                    h.OrgId = 256;
                    h.DiscountAmount = 3931m;
                    h.OrderDiscount = 50m;
                    h.TotalDue = 4835m;
                    h.TotalVolume = 148.7m;
                    h.TotalAmountPaid = 4605m;
                    h.TaxAmount = 230;
                    h.InvShipFlag = "Y";
                    h.SMSNumber = "000-012345";
                    h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
                    h.ShippingInstructions = "Order from ASC";
                })
                .AddPayment(p =>
                {
                    p.PaymentMethodName = "CARD";
                    // p.PaymentMethodId = 3;
                    p.PaymentAmount = 167.94m;
                    p.Date = new DateTime(2020, 11, 27, 8, 31, 44);
                    p.Paycode = "CARD";
                    p.PaymentType = "SALE";
                    p.CurrencyCode = "EUR";
                    p.AppliedDate = new DateTime(2020, 11, 27, 8, 31, 44);
                    p.PaymentReceived = 167.94m;
                    p.CreditCard.CardHolderName = "CARD HOLDER";
                    p.CreditCard.CardNumber = "0B11074741560000";
                    p.CreditCard.CardType = "CARD";
                    p.CreditCard.CardExpiryDate = new DateTime(2021, 11, 30, 23, 59, 59);

                })
                .AddItems(() =>
                    new SubmitRequestOrderLine[] {
                        new SubmitRequestOrderLine {
                            Sku = "4463",
                            Quantity = 2,
                            Amount = 54.19m,
                            EarnBase = 27.97m,
                            UnitEarnBase = 55.94m,
                            UnitVolume = 47.9m,
                            TotalRetailPrice = 61.36m,
                            TotalDiscountedPrice = 19.58m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "0242",
                            Quantity = 3,
                            Amount = 63.82m,
                            EarnBase = 21.97m,
                            UnitEarnBase = 65.91m,
                            UnitVolume = 53.85m,
                            TotalRetailPrice = 72.27m,
                            TotalDiscountedPrice = 23.07m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "0260",
                            Quantity = 1,
                            Amount = 15.76m,
                            EarnBase = 7.13m,
                            UnitEarnBase = 7.13m,
                            UnitVolume = 7.7m,
                            TotalRetailPrice = 14.81m,
                            TotalDiscountedPrice = 2.5m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "2864",
                            Quantity = 1,
                            Amount = 21.29m,
                            EarnBase = 21.97m,
                            UnitEarnBase = 21.97m,
                            UnitVolume = 22.95m,
                            TotalRetailPrice = 21.97m,
                            TotalDiscountedPrice = 7.69m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "096K",
                            Quantity = 1,
                            Amount = 12.8839m,
                            EarnBase = 13.33m,
                            UnitEarnBase = 13.33m,
                            UnitVolume = 14m,
                            TotalRetailPrice = 14.63m,
                            TotalDiscountedPrice = 4.67m
                        }
                    }
                );

            new SubmitRequestBuilder();

            // Pre-validate
            Assert.NotNull(_adapter);

            // Perform

            //Post-validate
        }
    }
}
