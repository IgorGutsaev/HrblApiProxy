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
            //.WithUri("https://herbalife-econnectslc.hrbl.com/Order/HLOnlineOrdering/prod/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
            .WithServiceConsumer(SERVICE_CONSUMER)
            .WithOrganizationId(73)
            .WithCredentials("hlfnord", "welcome123") // prs + ts3 + ts1
            //.WithCredentials("hlfnord", "F1uT2H1n@0rd")
            .WithPollSettings(x => {
                x.Input_for_GetVolumePoints = new (string distributorId, DateTime month)[] { ("U512180202", DateTime.UtcNow), ("7918180560", DateTime.UtcNow) };
                x.Input_for_GetProfile = new string[] { "U512180202", "7918180560" };
                x.Input_for_GetDistributorTIN = new (string distributorId, string country)[] { ("D1040636", "ID"), ("D2175964", "ID") };
                x.Input_for_GetDistributorDiscount = new (string distributorId, DateTime month, string country)[] { ("7918180560", DateTime.UtcNow.AddMonths(-1), "RU"), ("7918180560", DateTime.UtcNow, "RU"), ("MYY0013707", DateTime.UtcNow.AddMonths(-1), "MY"), ("7520260873", DateTime.UtcNow, "GE") };
                x.Input_for_GetSku = new (string sku, string warehouse)[] { ("0006", "TW"), ("4464", "LV"), ("0141", "L8") };
                x.Input_for_GetProductInventory = new string[] { "VN", "KR" };
            })
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);

        public string CardTokenizationUrl => "https://dsgtokenservervqa1.hrbl.net/hrbl/json/tokenize";
        public string CardTokenizationLogin => "FusionUser";
        public string CardTokenizationPassword => "protegrity4";
    }
}