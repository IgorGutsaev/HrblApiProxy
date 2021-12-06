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

           // HpsPaymentPayload payload = JsonConvert.DeserializeObject<HpsPaymentPayload>("{\"Country\":\"KR\",\"OrderNumber\":\"K6K3994613\",\"ClientRefNum\":\"KRBUSAS1\",\"DistributorId\":\"VA00248957\",\"PayCode\":\"BC\",\"Currency\":\"KRW\",\"Amount\":\"34027.0\",\"CardHolderName\":\"HERB 테 스 트\",\"CreditCardNum\":\"5E99146735510096\",\"ExpiryDate\":\"2024-05\",\"CVV2\":\"11\",\"PayeeID\":\"800101\",\"Address1\":\"111/16 PHUOC LONG - PHUOC LONG\",\"City\":\"KHANH HOA\",\"PostalCode\":\"65\",\"ProcessingLocation\":\"K6\",\"OrderType\":\"\",\"Installments\":0}");

            // Perform
            string result = await _adapter.HpsPaymentGateway(new HpsPaymentPayload {
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
            //SubmitRequest request = JsonConvert.DeserializeObject<SubmitRequest>("{\"ServiceConsumer\":\"AAKIOSK\",\"OrderHeaders\":{\"DistributorId\":\"80112512\",\"CustomerName\":\"ЕКАТЕРИНА КОВАЛЕВА\",\"ExternalOrderNumber\":\"RGK7257708\",\"TotalDue\":6612.78,\"OrderMonth\":\"2109\",\"SalesChannelCode\":\"AUTOSTORE\",\"OrderDate\":\"2021-09-07T09:26:31\",\"PricingDate\":\"2021-09-07T13:26:26\",\"TotalVolume\":117.6,\"OrderTypeCode\":\"RSO\",\"TotalAmountPaid\":6612.78,\"OrderPaymentStatus\":\"PAID\",\"OrderDiscount\":50.0,\"CountryCode\":\"RU\",\"WareHouseCode\":\"RG\",\"ProcessingLocation\":\"RG\",\"ShippingMethodCode\":\"PU1\",\"City\":\"Москва\",\"InvShipFlag\":\"Y\",\"OrgId\":\"178\",\"OrderTypeId\":\"5836\",\"PostalCode\":\"108811\",\"Province\":null,\"Address1\":\"п. Московский, Киевское шоссе, 22й км, д. 6, стр. 1\",\"Address2\":\"\",\"State\":null,\"ShippingInstructions\":\"Order from ASC\",\"PickupName\":null,\"TaxAmount\":1102.14,\"DiscountAmount\":4107.39,\"OrderConfirmEmail\":\"KATERINA_VK@LIST.RU\",\"WillCallFlag\":\"N\",\"OrderPurpose\":\"\",\"SlidingDiscount\":0.0,\"OrderSource\":\"KIOSK\",\"Balance\":0.0,\"TotalRetailPrice\":\"0\",\"ChrAttribute4\":null,\"ChrAttribute3\":\"N\",\"ChrAttribute6\":\"QR\",\"ChrAttribute5\":null,\"Phone\":null,\"Notes\":null,\"SMSNumber\":\"9104393139\",\"SMSAction\":null,\"SMSRole\":null,\"OrderSubType\":null},\"OrderLine\":[{\"SKU\":\"0003\",\"Quantity\":1.0,\"LineAmount\":605.47,\"UnitVolume\":12.5,\"EarnBase\":992.32,\"TotalRetailPrice\":1101.63,\"TotalDiscountedPrice\":496.16,\"ProductType\":\"P\"},{\"SKU\":\"0006\",\"Quantity\":1.0,\"LineAmount\":983.86,\"UnitVolume\":24.95,\"EarnBase\":1612.58,\"TotalRetailPrice\":1790.15,\"TotalDiscountedPrice\":806.29,\"ProductType\":\"P\"},{\"SKU\":\"0022\",\"Quantity\":1.0,\"LineAmount\":650.79,\"UnitVolume\":15.5,\"EarnBase\":1066.67,\"TotalRetailPrice\":1184.13,\"TotalDiscountedPrice\":533.34,\"ProductType\":\"P\"},{\"SKU\":\"0260\",\"Quantity\":1.0,\"LineAmount\":807.78,\"UnitVolume\":7.7,\"EarnBase\":506.59,\"TotalRetailPrice\":1061.08,\"TotalDiscountedPrice\":253.3,\"ProductType\":\"P\"},{\"SKU\":\"1171\",\"Quantity\":1.0,\"LineAmount\":1085.37,\"UnitVolume\":23.95,\"EarnBase\":1778.95,\"TotalRetailPrice\":1974.85,\"TotalDiscountedPrice\":889.48,\"ProductType\":\"P\"},{\"SKU\":\"2273\",\"Quantity\":2.0,\"LineAmount\":858.88,\"UnitVolume\":9.5,\"EarnBase\":1407.8,\"TotalRetailPrice\":1562.78,\"TotalDiscountedPrice\":703.9,\"ProductType\":\"P\"},{\"SKU\":\"2669\",\"Quantity\":1.0,\"LineAmount\":518.49,\"UnitVolume\":14.0,\"EarnBase\":849.83,\"TotalRetailPrice\":943.41,\"TotalDiscountedPrice\":424.92,\"ProductType\":\"P\"}],\"OrderPayment\":{\"PaymentMethodName\":\"CARD\",\"PaymentStatus\":\"PAID\",\"PaymentMethodId\":null,\"PaymentAmount\":6612.78,\"PaymentDate\":\"2021-09-07T09:26:31\",\"Paycode\":\"MC\",\"PaymentType\":\"SALE\",\"CurrencyCode\":\"RUB\",\"AppliedDate\":\"2021-09-07T09:26:31\",\"ApprovalNumber\":\"53e1fa81-925e-4c3e-9b67-765578f70774\",\"PaymentReceived\":\"6612.78\",\"CreditCard\":{\"CardNumber\":\"4H58265476741111\",\"TrxApprovalNumber\":\"53e1fa81-925e-4c3e-9b67-765578\",\"CardType\":\"MC\",\"CardExpiryDate\":\"2022-09-30T23:59:59\",\"CardHolderName\":\"CARD HOLDER\",\"CardValidationType\":\"ONLINE\",\"CardHolderRelation\":\"SELF\"},\"AuthorizationType\":\"ONLINE\",\"VoidFlag\":\"N\",\"ClientRefNumber\":\"RUMSKRUMAS1\"},\"OrderNotes\":null,\"OrderAddress\":null,\"OrderPromotionLine\":null}");
            SubmitRequest request = JsonConvert.DeserializeObject<SubmitRequest>("{\"ServiceConsumer\":\"AAKIOSK\",\"OrderHeaders\":{\"DistributorId\":\"D2Y0000002\",\"CustomerName\":\"DEWIO VALIDATION\",\"ExternalOrderNumber\":\"DAK3339946\",\"TotalDue\":721083.0,\"OrderMonth\":\"2111\",\"SalesChannelCode\":\"AUTOSTORE\",\"OrderDate\":\"2021-11-29T17:27:00\",\"PricingDate\":\"2021-11-29T18:26:58\",\"TotalVolume\":44.95,\"OrderTypeCode\":\"RSO\",\"TotalAmountPaid\":721083.0,\"OrderPaymentStatus\":\"PAID\",\"OrderDiscount\":25.0,\"CountryCode\":\"ID\",\"WareHouseCode\":\"FU\",\"ProcessingLocation\":\"DA\",\"ShippingMethodCode\":\"PU\",\"City\":\"JAKARTA SELATAN\",\"InvShipFlag\":\"Y\",\"OrgId\":\"259\",\"OrderTypeId\":\"2940\",\"PostalCode\":\"12560\",\"Province\":null,\"Address1\":\"CIBIS Nine Building 6th & Ground Floor\",\"Address2\":\"Jl. T.B. Simatupang No. 2, Unit K - N\",\"State\":\"DKI JAKARTA\",\"ShippingInstructions\":\"Order from ASC\",\"PickupName\":null,\"TaxAmount\":65553.0,\"DiscountAmount\":193250.0,\"OrderConfirmEmail\":\"DEWIO@HERBALIFE.COM\",\"WillCallFlag\":\"N\",\"OrderPurpose\":\"\",\"SlidingDiscount\":0.0,\"OrderSource\":\"KIOSK\",\"Balance\":0.0,\"TotalRetailPrice\":\"0\",\"ChrAttribute4\":null,\"ChrAttribute3\":\"N\",\"ChrAttribute6\":\"QR\",\"ChrAttribute5\":null,\"Phone\":null,\"Notes\":null,\"SMSNumber\":\"8111558373\",\"SMSAction\":null,\"SMSRole\":null,\"OrderSubType\":null},\"OrderLine\":[{\"SKU\":\"0119\",\"Quantity\":1.0,\"LineAmount\":333315.68,\"UnitVolume\":20.0,\"EarnBase\":393000.0,\"TotalRetailPrice\":421000.0,\"TotalDiscountedPrice\":98250.0,\"ProductType\":\"P\"},{\"SKU\":\"1065\",\"Quantity\":1.0,\"LineAmount\":322214.32,\"UnitVolume\":24.95,\"EarnBase\":380000.0,\"TotalRetailPrice\":407000.0,\"TotalDiscountedPrice\":95000.0,\"ProductType\":\"P\"}],\"OrderPayment\":{\"PaymentMethodName\":\"CARD\",\"PaymentStatus\":\"PAID\",\"PaymentMethodId\":\"755\",\"PaymentAmount\":721083.0,\"PaymentDate\":\"2021-11-29T17:27:00\",\"Paycode\":\"AC\",\"PaymentType\":\"SALE\",\"CurrencyCode\":\"IDR\",\"AppliedDate\":\"2021-11-29T17:27:00\",\"ApprovalNumber\":\"d995277d-4789-40b8-8c39-cd7977104352\",\"PaymentReceived\":\"721083\",\"CreditCard\":{\"CardNumber\":\"553388******3485\",\"TrxApprovalNumber\":\"d995277d-4789-40b8-8c39-cd7977\",\"CardType\":\"OFF-US MASTERCARD\",\"CardExpiryDate\":\"2022-11-30T23:59:59\",\"CardHolderName\":\"CARD HOLDER\",\"CardValidationType\":\"ONLINE\",\"CardHolderRelation\":\"SELF\"},\"AuthorizationType\":\"ONLINE\",\"VoidFlag\":\"N\",\"ClientRefNumber\":\"IDJAKAS1\"},\"OrderNotes\":null,\"OrderAddress\":null,\"OrderPromotionLine\":null}");
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
