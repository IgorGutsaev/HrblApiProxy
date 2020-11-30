using Filuet.Hrbl.Ordering.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class ModelTest
    {
        [Fact]
        public void Test_SubmitRequest_OrderHeader_serialization()
        {
            // Prepare
            SubmitRequestHeader header = new SubmitRequestHeader
            {
                // Order details mandatory
                DistributorId = "7918180560",
                CustomerName = "Ivan Ivanov",
                ExternalOrderNumber = "C5K1234567",
                TotalDue = 123.45m,
                OrderMonth = new DateTime(2020, 11, 1),
                SalesChannelCode = "AutoStore",
                OrderDate = new DateTime(2020, 11, 27, 14, 43, 40),
                PricingDate = new DateTime(2020, 11, 27, 14, 40, 10),
                TotalVolume = 15,
                OrderTypeCode = "RSO",
                TotalAmountPaid = 123.45m,
                OrderPaymentStatus = "PAID",// "UNDERPAID" "OVERPAID"
                OrderDiscount = 35m,
                // Location payload mandatory
                CountryCode = "RU",
                WareHouseCode = "Foo",
                ProcessingLocation = "Bar",
                ShippingMethodCode = "Baz",
                City = "Moscow",
                InvShipFlag = "Y",
                OrgId = 1589,
                PostalCode = "125167",
                Province = "qux",
                Address1 = "quux",
                Address2 = "quuz",
                State = "corge",
                // Non-mandatory
                ShippingInstructions = "10/00-16/00",
                PickupName = "pckp",
                TaxAmount = 1.58m,
                DiscountAmount = 35m,
                OrderConfirmEmail = "ds@email.ru",
                OrderPurpose = "OP",
                OrderSource = "KIOSK",
                TotalRetailPrice = 123.45m,        // спорный момент, скорее не нужен	   = isAALatviaStub ? 0 : 99,    bool isAALatviaStub = string.Equals(salesChannelCode, "AUTOATTENDANT", StringComparison.InvariantCultureIgnoreCase);        
                ChrAttribute4 = "Chr4",   //(isPudo ? postamatCode : null)  bool isPudo = freightCode.Equals("BLD") || freightCode.Equals("BLO");
                ChrAttribute3 = "Chr3",        // isPudo ?"Y":"N"
                ChrAttribute6 = "Chr6",          // QR хардкод (для Балтики как минимум)
                ChrAttribute5 = "Chr5",      //(isPudo ? phone : null)
                Phone = "79262140000", // null по ум 
                Notes = "hello world!",        //string     не mandatory (null по-умолчанию)
                SMSNumber = "79262140000",                // null string
                SMSAction = "ORDER COMPLETION",   //"ORDER COMPLETION" по-умолчанию
                SMSRole = "DS",     //      "DS" по-умолчанию
                OrderSubType = "ost"     // "" по-умолчанию
            };

            // Perform
            string result = JsonConvert.SerializeObject(header);

            // Post-validate
            Assert.Equal("{\"DistributorId\":\"7918180560\",\"CustomerName\":\"Ivan Ivanov\",\"ExternalOrderNumber\":\"C5K1234567\",\"TotalDue\":\"123.45\",\"OrderMonth\":\"2011\",\"SalesChannelCode\":\"AutoStore\",\"OrderDate\":\"2020-11-27T14:43:40\",\"PricingDate\":\"2020-11-27T14:40:10\",\"TotalVolume\":\"15\",\"OrderTypeCode\":\"RSO\",\"TotalAmountPaid\":\"123.45\",\"OrderPaymentStatus\":\"PAID\",\"OrderDiscount\":\"35\",\"CountryCode\":\"RU\",\"WareHouseCode\":\"Foo\",\"ProcessingLocation\":\"Bar\",\"ShippingMethodCode\":\"Baz\",\"City\":\"Moscow\",\"InvShipFlag\":\"Y\",\"OrgId\":\"1589\",\"PostalCode\":\"125167\",\"Province\":\"qux\",\"Address1\":\"quux\",\"Address2\":\"quuz\",\"State\":\"corge\",\"ShippingInstructions\":\"10/00-16/00\",\"PickupName\":\"pckp\",\"TaxAmount\":\"1.58\",\"DiscountAmount\":\"35\",\"OrderConfirmEmail\":\"ds@email.ru\",\"WillCallFlag\":\"N\",\"OrderPurpose\":\"OP\",\"SlidingDiscount\":\"0\",\"OrderSource\":\"KIOSK\",\"Balance\":\"0\",\"TotalRetailPrice\":\"123.45\",\"ChrAttribute4\":\"Chr4\",\"ChrAttribute3\":\"Chr3\",\"ChrAttribute6\":\"Chr6\",\"ChrAttribute5\":\"Chr5\",\"Phone\":\"79262140000\",\"Notes\":\"hello world!\",\"SMSNumber\":\"79262140000\",\"SMSAction\":\"ORDER COMPLETION\",\"SMSRole\":\"DS\",\"OrderSubType\":\"ost\"}", result);
        }

        [Fact]
        public void Test_SubmitRequest_OrderLine_serialization()
        {
            // Prepare
            SubmitRequestOrderLine line = new SubmitRequestOrderLine
            {
                Sku = "0141",
                Quantity = 3,
                Amount = 12.34m,
                TotalDiscountedPrice = 1.12m,
                EarnBase = 12.56m,
                TotalRetailPrice = 14.11m,
                UnitVolume = 22.95m,
                UnitEarnBase = 12.56m // eq to EarnBase 
            };

            // Perform
            string result = JsonConvert.SerializeObject(line);

            // Post-validate
            Assert.Equal("{\"SKU\":\"0141\",\"Quantity\":\"3\",\"LineAmount\":\"12.34\",\"UnitVolume\":\"22.95\",\"EarnBase\":\"12.56\",\"UnitEarnBase\":\"12.56\",\"TotalRetailPrice\":\"14.11\",\"TotalDiscountedPrice\":\"1.12\"}", result);
        }

        [Fact]
        public void Test_SubmitRequest_Payment_serialization()
        {
            // Prepare
            SubmitRequestPayment payment = new SubmitRequestPayment
            {
                PaymentMethodName = "CARD",
                PaymentMethodId = 3,
                PaymentAmount = 10m,
                Date = DateTime.UtcNow,
                Paycode = "CARD",
                PaymentType = "SALE",
                CurrencyCode = "TWD",
                AppliedDate = DateTime.UtcNow,
                ApprovalNumber = "070070",
                PaymentReceived = 10m,
                CreditCard = new OrderSubmitPaymentCreditCard
                {
                    CardExpiryDate = DateTime.UtcNow.AddYears(1),
                    CardHolderName = "CARD HOLDER",
                    CardNumber = "4A37991092121000",
                    CardType = "VI",
                    TrxApprovalNumber = "59b2996d9f20435a9773aadfbda868" // 30 digits
                }
            };

            // Perform
            string result = JsonConvert.SerializeObject(payment);

            // Post-validate
            Assert.Equal("{\"PaymentMethodName\":\"CARD\",\"PaymentMethodId\":\"3\",\"PaymentAmount\":\"10\",\"PaymentDate\":\"2020-11-30T10:12:56\",\"Paycode\":\"CARD\",\"PaymentType\":\"SALE\",\"CurrencyCode\":\"TWD\",\"AppliedDate\":\"2020-11-30T10:12:56\",\"ApprovalNumber\":\"070070\",\"PaymentReceived\":\"10\",\"CreditCard\":{\"CardNumber\":\"4A37991092121000\",\"TrxApprovalNumber\":\"59b2996d9f20435a9773aadfbda868\",\"CardType\":\"VI\",\"CardExpiryDate\":\"2021-11-30T10:12:56\",\"CardHolderName\":\"CARD HOLDER\"}}", result);
        }
    }
}
