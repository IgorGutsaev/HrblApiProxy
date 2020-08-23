using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class InventoryTest : BaseTest
    {
        [Theory]
        [InlineData("5C", "0141", 2)]
        public async Task Test_Valid_sku_remains(string warehouse, string sku, uint quantity)
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

        /// TODO: Handle invalid sku remains
        [Theory]
        [InlineData("5C", "0000", 2)]
        public async Task Test_Inalid_sku_remains(string warehouse, string sku, uint quantity)
        {
            // Prepare
            // Pre-validate
            // Perform
            // Post-validate
        }

        /// TODO: Handle invalid warehouse remains
        [Theory]
        [InlineData("XX", "0141", 2)]
        public async Task Test_Inalid_warehouse_remains(string warehouse, string sku, uint quantity)
        {
            // Prepare
            // Pre-validate
            // Perform
            // Post-validate
        }
    }
}
