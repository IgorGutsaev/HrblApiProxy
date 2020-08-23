using Filuet.Fusion.SDK;
using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.Abstractions.Profile;
using Filuet.Hrbl.Ordering.Abstractions.Warehouse;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class LimitsTest : BaseTest
    {
        [Theory]
        [InlineData("7918180560", "ru")]
        public async Task Test_Get_ds_fop_purchasing_limits(string distributorId, string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));
            Assert.False(string.IsNullOrWhiteSpace(country));

            // Perform
            FOPPurchasingLimits result = await _adapter.GetDSFOPPurchasingLimits(distributorId, country);

            // Post-validate
            Assert.NotNull(result);
            //Assert.Equal(distributorId, result.);
        }
    }
}