using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class ProfileTest : BaseTest
    {
        [Theory]
       // [InlineData("7918180560")]
        //[InlineData("U515120144")]
        [InlineData("VA00863126")]
       // [InlineData("HERB108388")] // DELETED member state
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

        [Theory]
        [InlineData("7918180560")]
        public async Task Test_Get_volume_points(string distributorId)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(distributorId));

            // Perform
            DistributorVolumePoints[] result = await _adapter.GetVolumePoints(distributorId, DateTime.Now);

            // Post-validate
            Assert.NotNull(result);
            //Assert.Equal(distributorId, result.Id);
        }
    }
}
