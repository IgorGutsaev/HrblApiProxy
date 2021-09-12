using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class TestDataGenerator
    {
        static public IEnumerable<object[]> GetHpsPayloadsWithValidNumber()
        {
            //yield return new object[] { "FR", "FM", "21Y0028945", "FMK1233692", "EUR", 0.1m, "IGOR GUTSAEV", "5B18869635313245", // tst
            //    "MC", new DateTime(2023, 12, 31), "xxx", null, 0, "VNHCMAS1", "26 Tran Cao Van, Ward 6", "Ho Chi Minh", "700000", "RSO" };

            yield return new object[] { "KR", "KF", "VA00248957", "KFK3994592", "KRW", 1m, "HERB 테 스 트", "4579730441394074", // tst
                "KB", new DateTime(2023, 12, 31), "11", "800101", 0, "KRSEOAS1", "Kangnam gu, 86-8 Non Hyun-2dong", "Seoul", "135010", "RSO" };
        }

        static public IEnumerable<object[]> GetHpsPayloadsWithCorruptedNumber()
        {
            yield return new object[] { "FR", "FM", "21Y0028945", "FMK1233692", "EUR", 1m, "TEST", "5B55607430513558", "MC", new DateTime(2022, 8, 31), "056", null, 0, "FRMARAS1", "19 RUE ALICE OZIER", "FORT DE FRANCE", "97200", "RSO" };
            
            yield return new object[] { "AZ", "IA", "AZ18360193" , "IAK1138803", "AZN", 110.37m, "ADILFARZ RAMAZANOV", "6D28712432325710", "VI", new DateTime(2020, 11, 30), "97", null, 0, "AZSEOAS1", "APARTMENT 35", "BAKU CITY", "01078", "RSO" };
        }

        static public IEnumerable<object[]> GetPricingRequest()
        {
            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "DA";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "HLINTERNET";
            //          h.CurrencyCode = "IDR";
            //          h.DistributorId = "D2440603";
            //          h.Warehouse = "D1";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU";
            //          h.CountryCode = "ID";
            //          h.PostalCode = "12560";
            //          h.City = "JAKARTA SELATAN";
            //          h.State = "DKI JAKARTA";
            //          h.Province = "PASAR MINGGU";
            //          h.County = "PEKAYON JAYA";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "CIBIS Nine Building 6th & Ground Floor";
            //          h.Address2 = "Jl. T.B. Simatupang No. 2, Unit K - N";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "2631",
            //                Quantity = 1,
            //                ProcessingLocation = "DA"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "DA";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "HLINTERNET";
            //          h.CurrencyCode = "IDR";
            //          h.DistributorId = "D2440603";
            //          h.Warehouse = "D1";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU";
            //          h.CountryCode = "ID";
            //          h.PostalCode = "12560";
            //          h.City = "JAKARTA SELATAN";
            //          h.State = "DKI JAKARTA";
            //          h.Province = "PASAR MINGGU";
            //          h.County = "PEKAYON JAYA";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "CIBIS Nine Building 6th & Ground Floor";
            //          h.Address2 = "Jl. T.B. Simatupang No. 2, Unit K - N";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "2631",
            //                Quantity = 1,
            //                ProcessingLocation = "DA"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "5E";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "RUB";
            //          h.DistributorId = "7918180560";
            //          h.Warehouse = "5E";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU1";
            //          h.CountryCode = "RU";
            //          h.PostalCode = "630005";
            //          h.City = "г. Новосибирск";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "ул. Фрунзе дом.86";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "0141",
            //                Quantity = 1,
            //                ProcessingLocation = "5E"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "LR";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "EUR";
            //          h.DistributorId = "U515170226";
            //          h.Warehouse = "LR";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU1";
            //          h.CountryCode = "LV";
            //          h.PostalCode = "LV-1073";
            //          h.City = "Riga";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "Piedrujas street, 7a";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "0003",
            //                Quantity = 1,
            //                ProcessingLocation = "LR"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "AI";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "AMD";
            //          h.DistributorId = "7412280728";
            //          h.Warehouse = "ANT";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "AME"; // - в ереван "AMR" - в другие регионы
            //          h.CountryCode = "AM";
            //          h.PostalCode = "0019";
            //          h.City = "Yerevan";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "FILUET ARMENIA";
            //          h.Address2 = "6 BAGHRAMYAN AVE,";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "0006",
            //                Quantity = 1,
            //                ProcessingLocation = "AI"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "GG";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "GEL";
            //          h.DistributorId = "7412280728";
            //          h.Warehouse = "GG";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "WI";
            //          h.CountryCode = "GE";
            //          h.PostalCode = "0162";
            //          h.City = "Tbilisi";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "Ground Floor, 4a Tamarishvili Str., Vake District";
            //          h.Address2 = "";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "0006",
            //                Quantity = 1,
            //                ProcessingLocation = "GG"
            //            }
            //           }).Build() };

             yield return new PricingRequest[] { JsonConvert.DeserializeObject<PricingRequest>("{\"ServiceConsumer\":null,\"OrderPriceHeader\":{\"OrderSource\":\"Internet\",\"OrgID\":259,\"OrderTypeID\":2940,\"ExternalOrderNumber\":null,\"DistributorId\":\"UZ20102558\",\"Warehouse\":\"LR\",\"ProcessingLocation\":\"IL\",\"FreightCode\":\"PU\",\"CountryCode\":\"LV\",\"OrderMonth\":\"2109\",\"OrderCategory\":\"RSO\",\"OrderType\":\"IE\",\"PriceDate\":\"2021-09-08T10:31:05\",\"OrderDate\":\"2021-09-08T10:31:05\",\"CurrencyCode\":\"EUR\",\"PostalCode\":null,\"City\":\"Riga\",\"State\":\"\",\"Province\":\"\",\"County\":\"\",\"Address1\":\"Piedrujas 7A, LV-1073\",\"Address2\":\"\",\"Address3\":null,\"Address4\":null},\"OrderPriceLines\":[{\"ProcessingLocation\":\"IL\",\"SellingSKU\":\"N345\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"ProcessingLocation\":\"IL\",\"SellingSKU\":\"6473\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"ProcessingLocation\":\"IL\",\"SellingSKU\":\"N338\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"ProcessingLocation\":\"IL\",\"SellingSKU\":\"003U\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"ProcessingLocation\":\"IL\",\"SellingSKU\":\"5451\",\"ProductType\":\"P\",\"OrderedQty\":1.0}]}") };
        }
    }
}
