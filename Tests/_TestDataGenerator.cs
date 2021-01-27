using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class TestDataGenerator
    {
        static public IEnumerable<object[]> GetHpsPayloadsWithCorruptedNumber()
        {
            yield return new object[] { "FR", "FM", "21Y0028945", "FMK1233692", "EUR", 10m, "TEST", "5B55607430513558", "MC", new DateTime(2022, 8, 31), "056", null, 0, "FRMARAS1", "19 RUE ALICE OZIER", "FORT DE FRANCE", "97200", "RSO" };
            
            yield return new object[] { "AZ", "IA", "AZ18360193" , "IAK1138803", "AZN", 110.37m, "ADILFARZ RAMAZANOV", "6D28712432325710", "VI", new DateTime(2020, 11, 30), "97", null, 0, "AZSEOAS1", "APARTMENT 35", "BAKU CITY", "01078", "RSO" };
        }

        static public IEnumerable<object[]> GetPricingRequest()
        {
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

            yield return new object[] { new PricingRequestBuilder()
                .AddServiceConsumer(BaseTest.SERVICE_CONSUMER).AddHeader(h =>
                  {
                      h.ProcessingLocation = "AI";
                      h.ExternalOrderNumber = null;
                      h.OrderSource = "KIOSK";
                      h.CurrencyCode = "AMD";
                      h.DistributorId = "7412280728";
                      h.Warehouse = "ANT";
                      h.OrderMonth = DateTime.UtcNow.AddDays(-1);
                      h.FreightCode = "AME"; // - в ереван "AMR" - в другие регионы
                      h.CountryCode = "AM";
                      h.PostalCode = "0019";
                      h.City = "Yerevan";
                      h.OrderCategory = "RSO";
                      h.OrderType = "RSO";
                      h.PriceDate = DateTime.UtcNow;
                      h.OrderDate = DateTime.UtcNow;
                      h.Address1 = "FILUET ARMENIA";
                      h.Address2 = "6 BAGHRAMYAN AVE,";                     
                  })
                  .AddItems(() =>
                       new PricingRequestLine[] {
                        new PricingRequestLine {
                            Sku = "0006",
                            Quantity = 1,
                            ProcessingLocation = "AI"
                        }
                       }).Build() };
        }
    }
}
