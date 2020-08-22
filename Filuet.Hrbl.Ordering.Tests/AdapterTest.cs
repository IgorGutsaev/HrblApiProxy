using Filuet.Hrbl.Ordering.Abstractions.Warehouse;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class AdapterTest : BaseTest
    {
        [Fact]
        public void Test_Constructor()
        {
            // Prepare

            // Pre-validate

            // Perform

            // Post-validate
        }

        [Theory]
        [InlineData("5C", "0141", 2)]
        public async Task Test_SkuRemains(string warehouse, string sku, uint quantity)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(warehouse));
            Assert.False(string.IsNullOrWhiteSpace(sku));
            Assert.True(quantity > 0);

            // Perform
            SkuInventory[] result = await _adapter.GetSkuAvailability(warehouse, sku, quantity);

            // Post-validate
            Assert.NotNull(result);
            Assert.True(result.Length == 1);
            Assert.True(result[0].Sku == sku);
        }
    }
}
