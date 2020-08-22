using Filuet.Hrbl.Ordering.Abstractions.Profile;
using Filuet.Hrbl.Ordering.Abstractions.Warehouse;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class ProfileTest : BaseTest
    {
        [Theory]
        [InlineData("7918180560")]
        public async Task Test_Get_profile(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            DistributorProfile result = await _adapter.GetProfile(distributorId);

            // Post-validate
            Assert.NotNull(result);
            Assert.Equal(distributorId, result.Id);
        }
    }
}
