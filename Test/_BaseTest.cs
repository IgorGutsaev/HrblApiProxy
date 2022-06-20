using Filuet.Hrbl.Ordering.Adapter;
using System;

namespace Filuet.Hrbl.Ordering.Tests
{
    public abstract class BaseTest
    {
        public const string SERVICE_CONSUMER = "AAKIOSK";

        private HrblOrderingAdapterSettings _defaultSettings =>
            new HrblOrderingAdapterSettingsBuilder()
            .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts3/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts1/")
            .WithUri("https://herbalife-econnectslc.hrbl.com/Order/HLOnlineOrdering/prod/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
            .WithServiceConsumer(SERVICE_CONSUMER)
            .WithOrganizationId(73)
            //.WithCredentials("hlfnord", "welcome123") // prs + ts3 + ts1
            .WithCredentials("hlfnord", "F1uT2H1n@0rd")
            .WithPollSettings(x => {
                x.Input_for_GetVolumePoints = new (string distributorId, DateTime month)[] { ("U512180202", DateTime.UtcNow), ("7918180560", DateTime.UtcNow) };
                x.Input_for_GetProfile = new string[] { "U512180202", "7918180560" };
                x.Input_for_GetDistributorTIN = new (string distributorId, string country)[] { ("D1040636", "ID"), ("D2175964", "ID") };
                x.Input_for_GetDistributorDiscount = new (string distributorId, DateTime month, string country)[] { ("7918180560", DateTime.UtcNow.AddMonths(-1), "RU"), ("7918180560", DateTime.UtcNow, "RU"), ("MYY0013707", DateTime.UtcNow.AddMonths(-1), "MY"), ("7520260873", DateTime.UtcNow, "GE") };
                x.Input_for_GetSku = new (string sku, string warehouse)[] { ("0006", "TW"), ("4464", "LV"), ("0141", "L8") };
                x.Input_for_GetProductInventory = new string[] { "VN", "KR" };
                x.Input_for_GetDSFOPPurchasingLimits = new (string distributorId, string country)[] { ("D2442080", "ID"), ("U512180202", "LV") };
                x.Input_for_GetCashLimit = new (string distributorId, string country)[] { ("VA00251288", "VN"), ("u512180202", "LV") };
                x.Input_for_GetDualMonth = new string[] { "KH", "IN", "IL" };
                x.Input_for_GetConversationRate = new (string exchangeRateType, string fromCurrency, string toCurrency)[] { ("HL Cambodia NTS FX", "USD",  "KHR") };
                x.Input_for_GetPricingRequests = new string[] { "{\"ServiceConsumer\":\"AAKIOSK\",\"OrderPriceHeader\":{\"OrderSource\":\"KIOSK\",\"ExternalOrderNumber\":null,\"DistributorId\":\"7918180560\",\"Warehouse\":\"0K\",\"ProcessingLocation\":\"0K\",\"FreightCode\":\"PU1\",\"CountryCode\":\"IN\",\"OrderMonth\":\"2203\",\"OrderCategory\":\"RSO\",\"OrderType\":\"RSO\",\"PriceDate\":\"2022-03-12T08:05:23\",\"OrderDate\":\"2022-03-12T08:05:23\",\"CurrencyCode\":\"INR\",\"OrderSubType\":\"\",\"PostalCode\":\"560025\",\"City\":\"Bangalore\",\"State\":\"Karnataka\",\"Province\":\"\",\"County\":\"\",\"Address1\":\"HERBALIFE INTERNATIONAL INDIA (P) LTD, CONDOR MIRAGE,\",\"Address2\":\"101/1, RICHMOND ROAD, RICHMOND TOWN, BANGALORE-560025\",\"Address3\":null,\"Address4\":null,\"OrgID\":253,\"OrderTypeID\":2803},\"OrderPriceLines\":[{\"ProcessingLocation\":\"0K\",\"SellingSKU\":\"154K\",\"ProductType\":\"P\",\"OrderedQty\":1.0},{\"ProcessingLocation\":\"0K\",\"SellingSKU\":\"183k\",\"ProductType\":\"P\",\"OrderedQty\":1.0}]}", "{\"ServiceConsumer\":\"AAKIOSK\",\"OrderPriceHeader\":{\"OrderSource\":\"KIOSK\",\"ExternalOrderNumber\":null,\"DistributorId\":\"MY706332\",\"Warehouse\":\"D1\",\"ProcessingLocation\":\"DA\",\"FreightCode\":\"PU\",\"CountryCode\":\"ID\",\"OrderMonth\":\"2206\",\"OrderCategory\":\"RSO\",\"OrderType\":\"RSO\",\"PriceDate\":\"2022-06-20T11:13:36\",\"OrderDate\":\"2022-06-20T11:13:36\",\"CurrencyCode\":\"IDR\",\"OrderSubType\":null,\"PostalCode\":\"12560\",\"City\":\"JAKARTA SELATAN\",\"State\":\"DKI JAKARTA\",\"Province\":\"PASAR MINGGU\",\"County\":\"PEKAYON JAYA\",\"Address1\":\"CIBIS Nine Building 6th & Ground Floor\",\"Address2\":\"Jl. T.B. Simatupang No. 2, Unit K - N\",\"Address3\":null,\"Address4\":null,\"OrgID\":259,\"OrderTypeID\":2940},\"OrderPriceLines\":[{\"ProcessingLocation\":\"DA\",\"SellingSKU\":\"0118\",\"ProductType\":\"P\",\"OrderedQty\":1.0}]}" };
            })
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);

        public string CardTokenizationUrl => "https://dsgtokenservervqa1.hrbl.net/hrbl/json/tokenize";
        public string CardTokenizationLogin => "FusionUser";
        public string CardTokenizationPassword => "protegrity4";
    }
}