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
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetPricingRequest), MemberType = typeof(TestDataGenerator))]
        public async Task Test_Pricing_request__Correct_response(object input)
        {
            // Prepare
            PricingRequest request = (PricingRequest)input;

            // Pre-validate
            Assert.NotNull(request);

            // Perform
            PricingResponse response = await _adapter.GetPriceDetails(request);

            // Post-validate
            Assert.NotNull(response);
            Assert.Equal(request.Header.DistributorId, response.Header.DistributorId);
            Assert.True(!string.IsNullOrWhiteSpace(response.Header.ExternalOrderNumber));
            if (!string.IsNullOrWhiteSpace(request.Header.ExternalOrderNumber))
                Assert.Equal(request.Header.ExternalOrderNumber, response.Header.ExternalOrderNumber);
            Assert.Equal(request.Lines.Length, response.Lines.Length);
            Assert.True(response.Header.TotalDue > 0);
            Assert.True(response.Header.VolumePoints > 0);
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
                    h.DistributorId = "U5005958";
                    h.CustomerName = "DIANA ZAHAREVSKA";
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
                    h.ExternalOrderNumber = "LRK1334836";
                    h.OrderTypeCode = "RSO";
                    h.PricingDate = new DateTime(2020, 11, 17, 17, 49, 13);
                    h.OrderDate = new DateTime(2020, 11, 17, 17, 49, 13);
                    h.OrgId = 294;
                    h.DiscountAmount = 0m;// 57.51m;
                    h.OrderDiscountPercent = 50m;
                    h.TotalDue = 9.36m;
                    h.TotalVolume = 12.5m;
                    h.TotalAmountPaid = 9.36m;
                    h.OrderPaymentStatus = "PAID";
                   // h.TaxAmount = 1.62m;
                    //h.FreightCharges = 9.13m;
                    h.InvShipFlag = "Y";
                    h.SMSNumber = "26346468";
                    h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
                    h.ShippingInstructions = "Order from ASC";
                })
                .AddPayment(p =>
                {
                    p.PaymentMethodName = "CARD";
                    p.PaymentStatus = "PAID";
                    p.PaymentMethodId = null; // Empty for LV
                    p.PaymentAmount = 9.36m;
                    p.Date = new DateTime(2020, 11, 17, 17, 49, 13);
                    p.Paycode = "CARD";
                    p.PaymentType = "SALE";
                    p.CurrencyCode = "EUR";
                    p.AppliedDate = new DateTime(2020, 11, 17, 17, 49, 13);
                    p.PaymentReceived = 9.36m;
                    p.CreditCard.CardHolderName = "CARD HOLDER";
                    p.CreditCard.CardNumber = "0B11074741560000";
                    p.CreditCard.CardType = "CARD";
                    p.CreditCard.CardExpiryDate = new DateTime(2021, 11, 30, 23, 59, 59);
                    p.CreditCard.TrxApprovalNumber = "51188";
                    p.ClientRefNumber = "LVRIGAS3";
                    p.ApprovalNumber = "51188";
                })
                .AddItems(() =>
                    new SubmitRequestOrderLine[] {
                        new SubmitRequestOrderLine {
                            Sku = "0003",
                            Quantity = 1.0m,
                            Amount = 9.3568m,
                            EarnBase = 11.9m,
                            UnitVolume = 12.15m,
                            TotalRetailPrice = 13.05m,
                            TotalDiscountedPrice = 5.95m
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
