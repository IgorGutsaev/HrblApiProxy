using Filuet.Hrbl.Ordering.Adapter;
using System;
using System.Collections.Generic;
using System.Text;

namespace Filuet.Hrbl.Ordering.Tests
{
    public abstract class BaseTest
    {
        private HrblOrderingAdapterSettings _defaultSettings =>
            new HrblOrderingAdapterSettingsBuilder()
            .WithUri("https://herbalife-oegdevws.hrbl.com/Order/HLOnlineOrdering/ts3/")
            .WithServiceConsumer("AAKIOSK")
            .WithCredentials("hlfnord", "welcome123")
            .Build();

        public HrblOrderingAdapter _adapter => new HrblOrderingAdapter(_defaultSettings);
    }
}
