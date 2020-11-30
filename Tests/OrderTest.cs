using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Common;
using Newtonsoft.Json;
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
                    h.OrderSource = "KIOSK";
                    h.DistributorId = "U512310066";
                    h.CustomerName = "ILVA VISTERE";
                    h.SalesChannelCode = "AUTOSTORE";
                    //h.ReferenceNumber = "LVRIGAS3";
                    h.WareHouseCode = "LR";
                    h.ProcessingLocation = "LR";
                    h.OrderMonth = new DateTime(2020, 11, 1);
                    h.ShippingMethodCode = "PU1";
                    h.CountryCode = "LV";
                    h.PostalCode = "LV-1073";
                    h.Address1 = "Piedrujas street, 7a";
                    h.City = "Riga";
                    h.ExternalOrderNumber = "LRK1336319";
                    h.OrderTypeCode = "RSO";
                    //h.OrderTypeId = 2991, // isn't represented in rest api 
                    h.PricingDate = new DateTime(2020, 11, 27, 8, 31, 44);
                    h.OrderDate = new DateTime(2020, 11, 27, 8, 31, 44);
                    h.OrgId = 294;
                    h.DiscountAmount = 0m;// 57.51m;
                    h.OrderDiscountPercent = 35m;
                    h.TotalDue = 167.94m;
                    h.TotalVolume = 146.4m;
                    h.TotalAmountPaid = 167.94m;
                    h.OrderPaymentStatus = "PAID";
                    h.TaxAmount = 29.15m;
                    //h.FreightCharges = 9.13m;
                    h.InvShipFlag = "Y";
                    h.SMSNumber = "000-012345";
                    h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
                    h.ShippingInstructions = "Order from ASC";
                })
                .AddPayment(p =>
                {
                    p.PaymentMethodName = "CARD";
                    p.PaymentStatus = "PAID";
                    p.PaymentMethodId = string.Empty; // Empty for LV
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
                    p.CreditCard.TrxApprovalNumber = "51505";
                    p.ClientRefNumber = "LVRIGAS3";
                    p.ApprovalNumber = "51505";
                })
                .AddItems(() =>
                    new SubmitRequestOrderLine[] {
                        new SubmitRequestOrderLine {
                            Sku = "4463",
                            Quantity = 2,
                            Amount = 54.19m,
                            EarnBase = 27.97m,
                            UnitVolume = 23.95m,
                            TotalRetailPrice = 61.36m,
                            TotalDiscountedPrice = 19.58m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "0242",
                            Quantity = 3,
                            Amount = 63.82m,
                            EarnBase = 21.97m,
                            UnitVolume = 17.95m,
                            TotalRetailPrice = 72.27m,
                            TotalDiscountedPrice = 23.07m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "0260",
                            Quantity = 1,
                            Amount = 15.76m,
                            EarnBase = 7.13m,
                            UnitVolume = 7.7m,
                            TotalRetailPrice = 14.81m,
                            TotalDiscountedPrice = 2.5m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "2864",
                            Quantity = 1,
                            Amount = 21.29m,
                            EarnBase = 21.97m,
                            UnitVolume = 22.95m,
                            TotalRetailPrice = 21.97m,
                            TotalDiscountedPrice = 7.69m
                        },
                        new SubmitRequestOrderLine {
                            Sku = "096K",
                            Quantity = 1,
                            Amount = 12.8839m,
                            EarnBase = 13.33m,
                            UnitVolume = 14m,
                            TotalRetailPrice = 14.63m,
                            TotalDiscountedPrice = 4.67m
                        }
                    }
                );

            // Pre-validate
            Assert.NotNull(_adapter);
            SubmitRequest request = setupAction.CreateTargetAndInvoke().Build();
            Assert.NotNull(request);

            string data = JsonConvert.SerializeObject(request);
            // Perform
            string orderNumber = await _adapter.SubmitOrder(setupAction);

            //Post-validate
        }
    }
}
