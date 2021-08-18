using Filuet.Hrbl.Ordering.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Tests
{
    public abstract class BaseTest
    {
        public const string SERVICE_CONSUMER = "AAKIOSK";

        private HrblOrderingAdapterSettings _defaultSettings =>
            new HrblOrderingAdapterSettingsBuilder()
            .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts1/")
            //.WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/prs/")
            .WithServiceConsumer(SERVICE_CONSUMER)
            .WithOrganizationId(73)
            .WithCredentials("hlfnord", "welcome123") // prs + ts3
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);
    }
}
