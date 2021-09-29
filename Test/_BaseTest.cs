using Filuet.Hrbl.Ordering.Adapter;

namespace Filuet.Hrbl.Ordering.Tests
{
    public abstract class BaseTest
    {
        public const string SERVICE_CONSUMER = "AAKIOSK";

        private HrblOrderingAdapterSettings _defaultSettings =>
            new HrblOrderingAdapterSettingsBuilder()
           // .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts3/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts1/")
            //.WithUri("https://herbalife-econnectslc.hrbl.com/Order/HLOnlineOrdering/prod/")
            .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
            .WithServiceConsumer(SERVICE_CONSUMER)
            .WithOrganizationId(73)
           .WithCredentials("hlfnord", "welcome123") // prs + ts3 + ts1
           //.WithCredentials("hlfnord", "F1uT2H1n@0rd")
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);

        public string CardTokenizationUrl => "https://dsgtokenservervqa1.hrbl.net/hrbl/json/tokenize";
        public string CardTokenizationLogin => "FusionUser";
        public string CardTokenizationPassword => "protegrity4";
    }
}