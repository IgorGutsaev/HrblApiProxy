using Filuet.Fusion.SDK;
using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class LimitsTest : BaseTest
    {
        /// <summary>
        /// DSFOPPurchasingLimits
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country">Ship to country</param>
        /// <returns></returns>
        [Theory]
        //[InlineData("7918180560", "ru")]
        //[InlineData("HERB108388", "ru")]
        //[InlineData("MY048647", "my")]
        //[InlineData("D2719997", "my")]
        //[InlineData("S7131170", "my")]
        // [InlineData("S7Y0003968", "my")]
        //[InlineData("MY048647", "my")]
        [InlineData("80X008634", "ru")]
        public async Task Test_Get_ds_fop_purchasing_limits(string distributorId, string country)
        {
            //// Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));
            Assert.False(string.IsNullOrWhiteSpace(country));

            // Perform
            FOPPurchasingLimitsResult result = await _adapter.GetDSFOPPurchasingLimits(distributorId, country);

            // Post-validate
            Assert.NotNull(result);
        }

        /// <summary>
        /// DSCashLimit
        /// </summary>
        /// <param name="distributorId"></param>
        /// <param name="country"></param>
        /// <returns></returns>
        [Theory]
        [InlineData("7918180560", "ru")]
       // [InlineData("HERB108388", "ru")]
        public async Task Test_Get_ds_cash_limit(string distributorId, string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));
            Assert.False(string.IsNullOrWhiteSpace(country));

            // Perform
            DsCashLimitResult result = await _adapter.GetDsCashLimit(distributorId, country);

            // Post-validate
            Assert.NotNull(result);
        }
    }
}
