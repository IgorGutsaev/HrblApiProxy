using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Filuet.Hrbl.Ordering.Common;
using Filuet.Hrbl.Ordering.Test;
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
        [MemberData(nameof(TestDataGenerator.GetHpsPayloadsWithValidNumber), MemberType = typeof(TestDataGenerator))]
        public async Task Test_HPS_payment_with__Valid_card_number(
            string country, string processingLocation, string distributorId, string orderNumber, string currency, decimal amount,
            string cardHolderName, string cardNumber, string cardType, DateTime cardExpirationMonth, string cvv2, string payeeId,
            uint installments, string clientRefNum, string address, string city, string postalCode, string orderType)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
          //  Assert.NotEqual(_adapter.Environment, HrblEnvironment.Prod);

            // Perform
            string result =
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
             });

            // Post-validate
            Assert.NotNull(result);
           // Assert.Equal("Invalid card number", ex.Message);
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
            //// Prepare
            //Action<SubmitRequestBuilder> setupAction = (b) =>
            //    b.AddHeader(h =>
            //    {
            //        h.OrderSource = "KIOSK";
            //        h.DistributorId = "U5005958";
            //        h.CustomerName = "DIANA ZAHAREVSKA";
            //        h.SalesChannelCode = "AUTOSTORE";
            //        //h.ReferenceNumber = "LVRIGAS3";
            //        h.WareHouseCode = "LR";
            //        h.ProcessingLocation = "LR";
            //        h.OrderMonth = new DateTime(2021, 04, 1);
            //        h.ShippingMethodCode = "PU1";
            //        h.CountryCode = "LV";
            //        h.PostalCode = "LV-1073";
            //        h.Address1 = "Piedrujas street, 7a";
            //        h.City = "Riga";
            //        h.ExternalOrderNumber = "LRK1334837";
            //        h.OrderTypeCode = "RSO";
            //        h.PricingDate = new DateTime(2021, 04, 15, 07, 49, 13);
            //        h.OrderDate = new DateTime(2021, 04, 15, 07, 49, 13);
            //        h.OrgId = 294;
            //        h.DiscountAmount = 0m;// 57.51m;
            //        h.OrderDiscountPercent = 50m;
            //        h.TotalDue = 9.36m;
            //        h.TotalVolume = 12.5m;
            //        h.TotalAmountPaid = 9.36m;
            //        h.OrderPaymentStatus = "PAID";
            //        // h.TaxAmount = 1.62m;
            //        //h.FreightCharges = 9.13m;
            //        h.InvShipFlag = "Y";
            //        h.SMSNumber = "26346468";
            //        h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
            //        h.ShippingInstructions = "Order from ASC";
            //    })
            //    .AddPayment(p =>
            //    {
            //        p.PaymentMethodName = "CARD";
            //        p.PaymentStatus = "PAID";
            //        p.PaymentMethodId = null; // Empty for LV; 
            //        p.PaymentAmount = 9.36m;
            //        p.Date = new DateTime(2021, 04, 15, 07, 49, 13);
            //        p.Paycode = "CARD";
            //        p.PaymentType = "SALE";
            //        p.CurrencyCode = "EUR";
            //        p.AppliedDate = new DateTime(2021, 04, 15, 07, 49, 13);
            //        p.PaymentReceived = 9.36m;
            //        p.CreditCard.CardHolderName = "CARD HOLDER";
            //        p.CreditCard.CardNumber = "0B11074741560000";
            //        p.CreditCard.CardType = "CARD";
            //        p.CreditCard.CardExpiryDate = new DateTime(2021, 11, 30, 23, 59, 59);
            //        p.CreditCard.TrxApprovalNumber = "51188";
            //        p.ClientRefNumber = "LVRIGAS3";
            //        p.ApprovalNumber = "51188";
            //    })
            //    .AddItems(() =>
            //        new SubmitRequestOrderLine[] {
            //            new SubmitRequestOrderLine {
            //                Sku = "0003",
            //                Quantity = 1.0m,
            //                Amount = 9.3568m,
            //                EarnBase = 11.9m,
            //                UnitVolume = 12.15m,
            //                TotalRetailPrice = 13.05m,
            //                TotalDiscountedPrice = 5.95m
            //            }
            //        }
            //    );

            //Action<SubmitRequestBuilder> setupAction = (b) =>
            //   b.AddHeader(h =>
            //   {
            //       h.OrderSource = "KIOSK";
            //       h.DistributorId = "7918180560";
            //       h.CustomerName = "ВАЛЕНТИНА ВАСИЛЬЕВА";
            //       h.SalesChannelCode = "AUTOSTORE";
            //        //h.ReferenceNumber = "LVRIGAS3";
            //       h.WareHouseCode = "6B";
            //       h.ProcessingLocation = "6B";
            //       h.OrderMonth = new DateTime(2021, 04, 1);
            //       h.ShippingMethodCode = "WI";
            //       h.CountryCode = "RU";
            //       h.PostalCode = "117042";
            //       h.Address1 = "ул. Веневская, д. 3А";
            //       h.City = "Москва";
            //       h.ExternalOrderNumber = "6BK7074781";
            //       h.OrderTypeCode = "RSO";
            //       h.PricingDate = new DateTime(2021, 4, 13, 9, 16, 13);
            //       h.OrderDate = new DateTime(2021, 4, 13, 9, 16, 13);
            //       h.OrgId = 178;
            //       h.DiscountAmount = 564.4m;
            //       h.OrderDiscountPercent = 35.0m;
            //       h.TotalDue = 1470.90m;
            //       h.TotalVolume = 24.95m;
            //       h.TotalAmountPaid = 1470.9m;
            //       h.OrderPaymentStatus = "PAID";
            //       h.TaxAmount = 245.15m;
            //       //h.FreightCharges = 9.13m;
            //       h.InvShipFlag = "Y";
            //       h.SMSNumber = "9262147116";
            //       h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
            //       h.ShippingInstructions = "Order from ASC";

            //       h.OrderSubType = "E";
            //   })
            //   .AddPayment(p =>
            //   {
            //       p.PaymentMethodName = "CARD";
            //       p.PaymentStatus = "PAID";
            //       p.PaymentMethodId = null; // Empty for LV; 
            //       p.PaymentAmount = 1470.9m;
            //       p.Date = new DateTime(2021, 04, 13, 9, 16, 30);
            //       p.Paycode = "CARD";
            //       p.PaymentType = "SALE";
            //       p.CurrencyCode = "RUB";
            //       p.AppliedDate = new DateTime(2021, 04, 13, 9, 16, 30);
            //       p.PaymentReceived = 1470.9m;
            //       p.CreditCard.CardHolderName = "CARD HOLDER";
            //       p.CreditCard.CardNumber = "4221 09** **** 7537";
            //       p.CreditCard.CardType = "MasterCard";
            //       p.CreditCard.CardExpiryDate = new DateTime(2022, 04, 30, 23, 59, 59);
            //       p.CreditCard.TrxApprovalNumber = "978f4b20-1c54-4019-a716-c06b0242de66";
            //       p.ClientRefNumber = "RUMSKBUTAS1";
            //       p.ApprovalNumber = "978f4b20-1c54-4019-a716-c06b0242de66";
            //   })
            //   .AddItems(() =>
            //       new SubmitRequestOrderLine[] {
            //            new SubmitRequestOrderLine {
            //                Sku = "0006",
            //                Quantity = 1.0m,
            //                Amount = 1225.75m,
            //                EarnBase = 1612.58m,
            //                UnitVolume = 24.95m,
            //                TotalRetailPrice = 1790.15m,
            //                TotalDiscountedPrice = 564.4m
            //            }
            //       }
            //   );

            // Pre-validate
            //Assert.NotNull(_adapter);
           // SubmitRequest request = setupAction.CreateTargetAndInvoke().Build();
            //Assert.NotNull(request);
            SubmitRequest request = JsonConvert.DeserializeObject<SubmitRequest>("{\"ServiceConsumer\":\"AAKIOSK\",\"OrderHeaders\":{\"DistributorId\":\"U511240476\",\"CustomerName\":\"EVA DANILENKO\",\"ExternalOrderNumber\":\"LRK1386058\",\"TotalDue\":174.16,\"OrderMonth\":\"2107\",\"SalesChannelCode\":\"AUTOATTENDANT\",\"OrderDate\":\"2021-07-13T11:17:46\",\"PricingDate\":\"2021-07-13T15:17:44\",\"TotalVolume\":201.85,\"OrderTypeCode\":\"RSO\",\"TotalAmountPaid\":174.16,\"OrderPaymentStatus\":\"PAID\",\"OrderDiscount\":50.0,\"CountryCode\":\"LV\",\"WareHouseCode\":\"LR\",\"ProcessingLocation\":\"LR\",\"ShippingMethodCode\":\"PU1\",\"City\":\"Riga\",\"InvShipFlag\":\"Y\",\"OrgId\":\"294\",\"OrderTypeId\":\"2991\",\"PostalCode\":\"LV-1073\",\"Province\":null,\"Address1\":\"Piedrujas iela, 7a\",\"Address2\":\"\",\"State\":null,\"ShippingInstructions\":\"Order from AA\",\"PickupName\":null,\"TaxAmount\":30.23,\"DiscountAmount\":105.09,\"OrderConfirmEmail\":null,\"WillCallFlag\":\"N\",\"OrderPurpose\":\"\",\"SlidingDiscount\":0.0,\"OrderSource\":\"KIOSK\",\"Balance\":0.0,\"TotalRetailPrice\":\"0\",\"ChrAttribute4\":null,\"ChrAttribute3\":\"N\",\"ChrAttribute6\":\"QR\",\"ChrAttribute5\":null,\"Phone\":null,\"Notes\":null,\"SMSNumber\":null,\"SMSAction\":null,\"SMSRole\":null,\"OrderSubType\":null},\"OrderLine\":[{\"SKU\":\"2864\",\"Quantity\":1.0,\"LineAmount\":14.29,\"UnitVolume\":22.95,\"EarnBase\":21.97,\"TotalRetailPrice\":24.1,\"TotalDiscountedPrice\":10.99,\"ProductType\":\"P\"},{\"SKU\":\"2273\",\"Quantity\":1.0,\"LineAmount\":8.04,\"UnitVolume\":9.5,\"EarnBase\":12.37,\"TotalRetailPrice\":13.57,\"TotalDiscountedPrice\":6.19,\"ProductType\":\"P\"},{\"SKU\":\"0242\",\"Quantity\":1.0,\"LineAmount\":14.28,\"UnitVolume\":17.95,\"EarnBase\":21.97,\"TotalRetailPrice\":24.09,\"TotalDiscountedPrice\":10.99,\"ProductType\":\"P\"},{\"SKU\":\"0260\",\"Quantity\":1.0,\"LineAmount\":11.96,\"UnitVolume\":7.7,\"EarnBase\":7.13,\"TotalRetailPrice\":14.81,\"TotalDiscountedPrice\":3.57,\"ProductType\":\"P\"},{\"SKU\":\"1065\",\"Quantity\":2.0,\"LineAmount\":33.96,\"UnitVolume\":24.95,\"EarnBase\":52.22,\"TotalRetailPrice\":57.28,\"TotalDiscountedPrice\":26.12,\"ProductType\":\"P\"},{\"SKU\":\"4467\",\"Quantity\":1.0,\"LineAmount\":18.19,\"UnitVolume\":23.95,\"EarnBase\":27.97,\"TotalRetailPrice\":30.68,\"TotalDiscountedPrice\":13.99,\"ProductType\":\"P\"},{\"SKU\":\"0106\",\"Quantity\":2.0,\"LineAmount\":43.2066,\"UnitVolume\":34.95,\"EarnBase\":66.48,\"TotalRetailPrice\":72.9,\"TotalDiscountedPrice\":33.24,\"ProductType\":\"P\"}],\"OrderPayment\":{\"PaymentMethodName\":\"CASH\",\"PaymentStatus\":\"PAID\",\"PaymentMethodId\":null,\"PaymentAmount\":174.16,\"PaymentDate\":\"2021-07-13T11:17:46\",\"Paycode\":\"CASH\",\"PaymentType\":\"SALE\",\"CurrencyCode\":\"EUR\",\"AppliedDate\":\"2021-07-13T11:17:46\",\"ApprovalNumber\":\"\",\"PaymentReceived\":\"174.16\",\"CreditCard\":null,\"AuthorizationType\":\"ONLINE\",\"VoidFlag\":\"N\",\"ClientRefNumber\":\"LVRIGAA1\"},\"OrderNotes\":null,\"OrderAddress\":null,\"OrderPromotionLine\":null}");
          //  string data = JsonConvert.SerializeObject(request);
          // Perform
            SubmitResponse result = await _adapter.SubmitOrder(request); //setupAction);

            //Post-validate
        }


        [Fact]
        public async Task Test_Submit_Order_AM()
        {
            // Prepare
            Action<SubmitRequestBuilder> setupAction = (b) =>
                b.AddHeader(h =>
                {
                    h.OrderSource = "KIOSK";
                    h.DistributorId = "V712310189";
                    h.CustomerName = "ANDREAS KALLI";
                    h.SalesChannelCode = "INTERNET";
                    //h.ReferenceNumber = "LVRIGAS3";
                    h.WareHouseCode = "AI";
                    h.ProcessingLocation = "FA";
                    h.OrderMonth = new DateTime(2020, 12, 1);
                    h.ShippingMethodCode = "PU";
                    h.CountryCode = "AM";
                    h.PostalCode = "0019";
                    h.Address1 = "FILUET ARMENIA";
                    h.Address2 = "6 BAGHRAMYAN AVE,";
                    h.City = "Yerevan";
                    h.ExternalOrderNumber = "FAK1079599";
                    h.OrderTypeCode = "RSO";
                    h.Phone = "0-45678";
                    h.PricingDate = DateTime.UtcNow;
                    h.OrderDate = DateTime.UtcNow;
                    h.OrgId = 2155;
                    h.DiscountAmount = 0m;// 57.51m;
                    h.OrderDiscountPercent = 50m;
                    h.TotalDue = 7714.19m;
                    h.TotalVolume = 24.95m;
                    h.TotalAmountPaid = 7714.19m;
                    h.OrderPaymentStatus = "PAID";
                    // h.TaxAmount = 1.62m;
                    //h.FreightCharges = 9.13m;
                    h.InvShipFlag = "Y";
                    h.SMSNumber = "";
                    h.OrderConfirmEmail = "igor.gutsaev@filuet.ru";
                    h.ShippingInstructions = "p:123-456789";
                })
                .AddPayment(p =>
                {
                    p.PaymentMethodName = "CARD";
                    p.PaymentStatus = "PAID";
                    p.PaymentMethodId = null; // Empty for LV; 
                    p.PaymentAmount = 7714.19m;
                    p.Date = DateTime.UtcNow;
                    p.Paycode = "CARD";
                    p.PaymentType = "SALE";
                    p.CurrencyCode = "AMD";
                    p.AppliedDate = DateTime.UtcNow;
                    p.PaymentReceived = 7714.19m;
                    p.CreditCard.CardHolderName = "CARD HOLDER";
                    p.CreditCard.CardNumber = "0B11074741560000";
                    p.CreditCard.CardType = "CARD";
                    p.CreditCard.CardExpiryDate = DateTime.UtcNow.AddYears(1);
                    p.CreditCard.TrxApprovalNumber = "51189";
                    //p.ClientRefNumber = "INTERNET";
                    p.ApprovalNumber = "51189";
                })
                .AddItems(() =>
                    new SubmitRequestOrderLine[] {
                        new SubmitRequestOrderLine {
                            Sku = "0006",
                            Quantity = 1.0m,
                            Amount = 7714.1851m,
                            EarnBase = 12244.5m,
                            UnitVolume = 24.95m,
                            TotalRetailPrice = 13076.68m,
                            TotalDiscountedPrice = 6122.25m
                        }
                    }
                );

            // Pre-validate
            Assert.NotNull(_adapter);
            SubmitRequest request = setupAction.CreateTargetAndInvoke().Build();
            Assert.NotNull(request);

            string data = JsonConvert.SerializeObject(request);
            // Perform
            SubmitResponse response = await _adapter.SubmitOrder(setupAction);
            //AIK1111732
            //Post-validate
        }

        [Theory]
        [InlineData("5536913792313245", "5B18869635313245")] // 5C23514072513245- Prod
        public void Test_Tokenization_Service_Tokenize_card(string cardNumber, string expected)
        {
            // Prepare

            // Pre-validate
            Assert.NotEmpty(cardNumber);
            Assert.NotEmpty(CardTokenizationUrl);
            Assert.NotEmpty(CardTokenizationLogin);
            Assert.NotEmpty(CardTokenizationPassword);

            // Perform
            string fact = CardTokenizer.Tokenize(cardNumber, CardTokenizationUrl, CardTokenizationLogin, CardTokenizationPassword);

            // Post-validate
            Assert.Equal(expected, fact);
        }
    }
}
