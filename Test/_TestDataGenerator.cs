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

            yield return new object[] { "AZ", "IA", "AZ18360193", "IAK1138803", "AZN", 110.37m, "ADILFARZ RAMAZANOV", "6D28712432325710", "VI", new DateTime(2020, 11, 30), "97", null, 0, "AZSEOAS1", "APARTMENT 35", "BAKU CITY", "01078", "RSO" };
        }

        static public IEnumerable<object[]> GetProductCatalog()
        {
            string data = "{\"076K\":1,\"4468\":1,\"110K\":1,\"012K\":1,\"0141\":1,\"0143\":1,\"1171\":1,\"0946\":1,\"0950\":1,\"0146\":1,\"2793\":1,\"005K\":1,\"0940\":1,\"1636\":1,\"2653\":1,\"0757\":1,\"0748\":1,\"0006\":1,\"1189\":1,\"178A\":1,\"2864\":1,\"2865\":1,\"2669\":1,\"1745\":1,\"2038\":1,\"0242\":1,\"001K\":1,\"002K\":1,\"0258\":1,\"0259\":1,\"0260\":1,\"0265\":1,\"0266\":1,\"0050\":1,\"0106\":1,\"0105\":1,\"0255\":1,\"0256\":1,\"0117\":1,\"3151\":1,\"3152\":1,\"0020\":1,\"0043\":1,\"1466\":1,\"0155\":1,\"2273\":1,\"0022\":1,\"0139\":1,\"0122\":1,\"0102\":1,\"0003\":1,\"0111\":1,\"0036\":1,\"1437\":1,\"3123\":1,\"2561\":1,\"2562\":1,\"2563\":1,\"2564\":1,\"2565\":1,\"0707\":1,\"0708\":1,\"0765\":1,\"0766\":1,\"0767\":1,\"0829\":1,\"0770\":1,\"0771\":1,\"0830\":1,\"0828\":1,\"0827\":1,\"0772\":1,\"0773\":1,\"0867\":1,\"043K\":1,\"449Z\":1,\"310A\":1,\"311A\":1,\"101M\":1,\"314A\":1,\"8347\":1,\"8501\":1,\"8459\":1,\"297A\":1,\"302A\":1,\"001N\":1,\"8414\":1,\"8697\":1,\"8787\":1,\"8573\":1,\"305A\":1,\"299A\":1,\"8550\":1,\"I004\":1,\"076A\":1,\"003A\":1,\"004A\":1,\"216A\":1,\"G755\":1,\"G756\":1,\"G758\":1,\"G759\":1,\"G760\":1,\"003U\":1,\"5001\":1,\"5942\":1,\"6510\":1,\"183N\":1,\"5089\":1,\"7051\":1,\"020N\":1,\"021N\":1,\"5817\":1,\"5818\":1,\"5995\":1,\"5819\":1,\"5060\":1,\"6398\":1,\"8669\":1,\"7905\":1,\"5652\":1,\"9598\":1,\"9749\":1,\"9828\":1,\"270U\":1,\"271U\":1,\"274U\":1,\"5463\":1,\"N182\":1,\"N151\":1,\"N133\":1,\"N017\":1,\"9182\":1,\"9183\":1,\"9185\":1,\"9187\":1,\"9189\":1,\"9191\":1,\"5953\":1,\"9193\":1,\"522U\":1,\"N878\":1,\"N739\":1,\"7887\":1,\"5802\":1,\"459N\":1,\"H228\":1,\"289N\":1,\"209N\":1,\"719A\":1,\"A727\":1,\"100M\":1,\"H230\":1,\"H231\":1,\"513M\":1,\"794N\":1,\"797N\":1,\"798N\":1,\"799N\":1,\"801N\":1,\"802N\":1,\"803N\":1,\"804N\":1,\"805N\":1,\"806N\":1,\"807N\":1,\"810N\":1,\"811N\":1,\"812N\":1,\"813N\":1,\"814N\":1,\"815N\":1,\"817N\":1,\"818N\":1,\"819N\":1,\"820N\":1,\"821N\":1,\"824N\":1,\"825N\":1,\"826N\":1,\"827N\":1,\"828N\":1,\"829N\":1,\"830N\":1,\"831N\":1,\"834N\":1,\"835N\":1,\"836N\":1,\"837N\":1,\"838N\":1,\"839N\":1,\"840N\":1,\"841N\":1,\"842N\":1,\"843N\":1,\"844N\":1,\"845N\":1,\"847N\":1,\"848N\":1,\"849N\":1,\"851N\":1,\"852N\":1,\"853N\":1,\"968L\":1}";

            yield return new object[] { "LR", JsonConvert.DeserializeObject<Dictionary<string, int>>(data) };
        }

        static public IEnumerable<object[]> GetPricingRequest()
        {
            yield return new object[] { new PricingRequestBuilder()
                .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
                  {
                      h.ProcessingLocation = "DA";
                      h.ExternalOrderNumber = null;
                      h.OrderSource = "KIOSK";
                      h.CurrencyCode = "IDR";
                      h.DistributorId = "MY706332";
                      h.Warehouse = "D1";
                      h.OrderMonth = DateTime.UtcNow.AddDays(-1);
                      h.FreightCode = "PU";
                      h.CountryCode = "ID";
                      h.PostalCode = "12560";
                      h.City = "JAKARTA SELATAN";
                      h.State = "DKI JAKARTA";
                      h.Province = "PASAR MINGGU";
                      h.County = "PEKAYON JAYA";
                      h.OrderCategory = "RSO";
                      h.OrderType = "RSO";
                      h.PriceDate = DateTime.UtcNow;
                      h.OrderDate = DateTime.UtcNow;
                      h.Address1 = "CIBIS Nine Building 6th & Ground Floor";
                      h.Address2 = "Jl. T.B. Simatupang No. 2, Unit K - N";
                  })
                  .AddItems(() =>
                       new PricingRequestLine[] {
                        new PricingRequestLine {
                            Sku = "0118",
                            Quantity = 1,
                            ProcessingLocation = "DA"
                        }
                       }).Build() };

            yield return new object[] { new PricingRequestBuilder()
                .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
                  {
                      h.ProcessingLocation = "DA";
                      h.ExternalOrderNumber = null;
                      h.OrderSource = "HLINTERNET";
                      h.CurrencyCode = "IDR";
                      h.DistributorId = "D2440603";
                      h.Warehouse = "D1";
                      h.OrderMonth = DateTime.UtcNow.AddDays(-1);
                      h.FreightCode = "PU";
                      h.CountryCode = "ID";
                      h.PostalCode = "12560";
                      h.City = "JAKARTA SELATAN";
                      h.State = "DKI JAKARTA";
                      h.Province = "PASAR MINGGU";
                      h.County = "PEKAYON JAYA";
                      h.OrderCategory = "RSO";
                      h.OrderType = "RSO";
                      h.PriceDate = DateTime.UtcNow;
                      h.OrderDate = DateTime.UtcNow;
                      h.Address1 = "CIBIS Nine Building 6th & Ground Floor";
                      h.Address2 = "Jl. T.B. Simatupang No. 2, Unit K - N";
                  })
                  .AddItems(() =>
                       new PricingRequestLine[] {
                        new PricingRequestLine {
                            Sku = "2631",
                            Quantity = 1,
                            ProcessingLocation = "DA"
                        }
                       }).Build() };

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

            yield return new object[] { new PricingRequestBuilder()
                .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
                  {
                      h.ProcessingLocation = "VN";
                      h.ExternalOrderNumber = null;
                      h.OrderSource = "KIOSK";
                      h.CurrencyCode = "VND";
                      h.DistributorId = "VNTESTID";
                      h.Warehouse = "VO";
                      h.OrderMonth = DateTime.UtcNow.AddDays(-1);
                      h.FreightCode = "PU";
                      h.CountryCode = "VN";
                      h.PostalCode = "700000";
                      h.City = "Ho Chi Minh";
                      h.OrderCategory = "RSO";
                      h.OrderType = "RSO";
                      h.PriceDate = DateTime.UtcNow;
                      h.OrderDate = DateTime.UtcNow;
                      h.Address1 = "26 Tran Cao Van";
                      h.Address2 = "Ward 6";
                  })
                  .AddItems(() =>
                       new PricingRequestLine[] {
                        new PricingRequestLine {
                            Sku = "0102",
                            Quantity = 1,
                            ProcessingLocation = "VN"
                        },
                        new PricingRequestLine {
                            Sku = "0141",
                            Quantity = 1,
                            ProcessingLocation = "VN"
                        }
                       }).Build() };
        }
    }
}
