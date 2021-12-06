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

            yield return new object[] { "KR", "KF", "VA00248957", "KFK3994592", "KRW", 1m, "HERB 테 스 트", "5E99146735510096", // tst
                "KB", new DateTime(2023, 12, 31), "11", "800101", 0, "KRSEOAS1", "Kangnam gu, 86-8 Non Hyun-2dong", "Seoul", "135052", "RSO" };
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
            //          h.ProcessingLocation = "6B";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "RUB";
            //          h.DistributorId = "7918180560";
            //          h.Warehouse = "RI";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU1";
            //          h.CountryCode = "RU";
            //          h.PostalCode = "117042";
            //          h.City = "Москва";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "ул. Краснобогатырская 90 стр.1";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "0141",
            //                Quantity = 1,
            //                ProcessingLocation = "RI"
            //            }
            //           }).Build() };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "DA";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "IDR";
            //          h.DistributorId = "MY706332";
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
            //                Sku = "0118",
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

            yield return new PricingRequest[] { JsonConvert.DeserializeObject<PricingRequest>("{\"OrderPriceHeader\":{\"DiscountPercent\":25,\"VolumePoints\":83.85,\"TotalFreightCharges\":12500.0,\"TotalPHCharges\":16860.0,\"TotalLogisticCharges\":0.0,\"TotalOtherCharges\":0.0,\"TotalTaxAmount\":132136.0,\"TotalRetailAmount\":1686000.0,\"TotalOrderAmount\":1321360.0,\"TotalDiscountAmount\":394000.0,\"TotalDue\":1453496.0,\"TotalProductRetail\":1292000.0,\"TotalLiteratureRetail\":0.0,\"TotalTaxBreakups\":{\"TaxBreakup\":[{\"TaxName\":\"VAT\",\"TaxValue\":132136.0,\"TaxRate\":null}]},\"ExternalOrderNumber\":\"\",\"DistributorId\":\"D2Y0000002\",\"Warehouse\":\"FU\",\"ProcessingLocation\":\"DA\",\"FreightCode\":\"PU\",\"CountryCode\":\"ID\",\"OrderMonth\":\"2112\",\"OrderCategory\":\"RSO\",\"OrderType\":\"RSO\",\"PriceDate\":\"2021-12-06T16:38:34\",\"OrderDate\":\"2021-12-06T16:38:34\",\"CurrencyCode\":\"IDR\",\"OrderSubType\":\"\",\"PostalCode\":\"12560\",\"City\":\"JAKARTA SELATAN\",\"State\":\"DKI JAKARTA\",\"Province\":\"\",\"County\":\"\",\"Address1\":\"CIBIS Nine Building 6th & Ground Floor\",\"Address2\":\"Jl.T.B.Simatupang No. 2, Unit K - N\",\"Address3\":\"\",\"Address4\":\"\",\"OrgID\":259,\"OrderTypeID\":null},\"OrderPriceLines\":[{\"PriceDate\":\"2021-12-06T16:38:34\",\"LineVolumePoints\":20.0,\"TotalEarnBase\":393000.0,\"TotalRetailPrice\":421000.0,\"UnitRetailPrice\":421000.0,\"LineTotalAmount\":330081.29,\"LineDueAmount\":363089.42,\"LineFreightCharges\":3121.29,\"LinePHCharges\":4210.0,\"LineLogisticCharges\":0.0,\"LineOtherCharges\":0.0,\"LineMiscCharges\":0.0,\"LineTaxAmount\":33008.13,\"LineDiscountAmount\":98250.0,\"Earnbase\":393000.0,\"UnitVolumePoints\":20.0,\"LineDiscountPrice\":322750.0,\"ProcessingLocation\":\"DA\",\"SellingSKU\":\"1029\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"PriceDate\":\"2021 - 12 - 06T16: 38:34\",\"LineVolumePoints\":9.0,\"TotalEarnBase\":193000.0,\"TotalRetailPrice\":206000.0,\"UnitRetailPrice\":206000.0,\"LineTotalAmount\":161337.28,\"LineDueAmount\":177471.01,\"LineFreightCharges\":1527.28,\"LinePHCharges\":2060.0,\"LineLogisticCharges\":0.0,\"LineOtherCharges\":0.0,\"LineMiscCharges\":0.0,\"LineTaxAmount\":16133.73,\"LineDiscountAmount\":48250.0,\"Earnbase\":193000.0,\"UnitVolumePoints\":9.0,\"LineDiscountPrice\":157750.0,\"ProcessingLocation\":\"DA\",\"SellingSKU\":\"0234\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"PriceDate\":\"2021 - 12 - 06T16: 38:34\",\"LineVolumePoints\":24.95,\"TotalEarnBase\":380000.0,\"TotalRetailPrice\":407000.0,\"UnitRetailPrice\":407000.0,\"LineTotalAmount\":319087.5,\"LineDueAmount\":350996.25,\"LineFreightCharges\":3017.5,\"LinePHCharges\":4070.0,\"LineLogisticCharges\":0.0,\"LineOtherCharges\":0.0,\"LineMiscCharges\":0.0,\"LineTaxAmount\":31908.75,\"LineDiscountAmount\":95000.0,\"Earnbase\":380000.0,\"UnitVolumePoints\":24.95,\"LineDiscountPrice\":312000.0,\"ProcessingLocation\":\"DA\",\"SellingSKU\":\"1065\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"PriceDate\":\"2021 - 12 - 06T16: 38:34\",\"LineVolumePoints\":29.9,\"TotalEarnBase\":610000.0,\"TotalRetailPrice\":652000.0,\"UnitRetailPrice\":652000.0,\"LineTotalAmount\":510853.93,\"LineDueAmount\":561939.32,\"LineFreightCharges\":4833.93,\"LinePHCharges\":6520.0,\"LineLogisticCharges\":0.0,\"LineOtherCharges\":0.0,\"LineMiscCharges\":0.0,\"LineTaxAmount\":51085.39,\"LineDiscountAmount\":152500.0,\"Earnbase\":610000.0,\"UnitVolumePoints\":29.9,\"LineDiscountPrice\":499500.0,\"ProcessingLocation\":\"DA\",\"SellingSKU\":\"2630\",\"ProductType\":\"P\",\"OrderedQty\":1.0}],\"Errors\":null}") };

            //yield return new object[] { new PricingRequestBuilder()
            //    .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
            //      {
            //          h.ProcessingLocation = "0K";
            //          h.ExternalOrderNumber = null;
            //          h.OrderSource = "KIOSK";
            //          h.CurrencyCode = "INR";
            //          h.DistributorId = "7918180560";
            //          h.Warehouse = "0K";
            //          h.OrderMonth = DateTime.UtcNow.AddDays(-1);
            //          h.FreightCode = "PU1";
            //          h.CountryCode = "IN";
            //          h.PostalCode = "560025";
            //          h.City = "Bangalore";
            //          h.OrderCategory = "RSO";
            //          h.OrderType = "RSO";
            //          h.PriceDate = DateTime.UtcNow;
            //          h.OrderDate = DateTime.UtcNow;
            //          h.Address1 = "HERBALIFE INTERNATIONAL INDIA (P) LTD, CONDOR MIRAGE,";
            //          h.Address2 = "101/1, RICHMOND ROAD, RICHMOND TOWN, BANGALORE-560025";
            //      })
            //      .AddItems(() =>
            //           new PricingRequestLine[] {
            //            new PricingRequestLine {
            //                Sku = "1248",
            //                Quantity = 1,
            //                ProcessingLocation = "0K"
            //            }
            //           }).Build() };
        }
    }
}
