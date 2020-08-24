using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class LocationTest : BaseTest
    {
        [Theory]
        [InlineData("ru")]
        public async Task Test_Get_ds_fop_purchasing_limits(string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(country));

            // Perform
            bool result = await _adapter.GetOrderDualMonthStatus(country);

            // Post-validate
            Assert.True(result);
        }
    }
}
