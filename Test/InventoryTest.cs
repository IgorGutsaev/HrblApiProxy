using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class InventoryTest : BaseTest
    {
        [Theory]
       // [InlineData("TW", "0006", 1)]
        //[InlineData("LR", "794N / 795N", 1)]]
        [InlineData("GG", "0006", 1)]
        //[InlineData("U7", "0006", 1)]
        //[InlineData("AI", "0006", 1)]
        public async Task Test_Valid_sku_remains(string warehouse, string sku, uint quantity)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(warehouse));
            Assert.False(string.IsNullOrWhiteSpace(sku));
            Assert.True(quantity > 0);

            // Perform
            SkuInventory result = await _adapter.GetSkuAvailability(warehouse, sku, quantity);

            // Post-validate
            Assert.NotNull(result);
            Assert.True(result.Sku == sku);
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


        [Theory]
        [InlineData("IN", "RSO")]
        public async Task Test_Product_Inventory(string country, string orderType)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(country));
            Assert.False(string.IsNullOrWhiteSpace(orderType));

            // Perform
            object result = await _adapter.GetProductInventory(country, orderType);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("LV", "RSO")]
        public async Task Test_Product_Catalog(string country, string orderType)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(country));
            Assert.False(string.IsNullOrWhiteSpace(orderType));

            // Perform
            object result = await _adapter.GetProductCatalog(country, orderType);

            // Post-validate
            Assert.NotNull(result);
        }
    }
}
