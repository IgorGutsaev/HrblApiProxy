using Filuet.Hrbl.Ordering.Abstractions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    class SSSSSku
    {
        public string Name { get; set; }
        public string Warehouse { get; set; }
    }

    class SSSSItem
    {
        [JsonProperty("Sku")]
        public SSSSSku Sku { get; set; }

        public int Count { get; set; }
        public int ProductType { get; set; }
    }

    public class InventoryTest : BaseTest
    {
        [Theory]
       // [InlineData("TW", "0006", 1)]
        //[InlineData("LR", "794N / 795N", 1)]]
        [InlineData("5C", "291А", 1)]
        //[InlineData("U7", "0006", 1)]
        //[InlineData("AI", "0006", 1)]
       // [InlineData("LV", "5438", 1)]
        public async Task Test_Valid_sku_remains(string warehouse, string sku, int quantity)
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
        
        [Theory]
        [MemberData(nameof(TestDataGenerator.GetProductCatalog), MemberType = typeof(TestDataGenerator))]
        public async Task Test_Valid_sku_remains_scope(string warehouse, Dictionary<string, int> items)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(warehouse));

            // Perform
            SkuInventory[] result = await _adapter.GetSkuAvailability(warehouse, items);

            // Post-validate
            Assert.NotNull(result);
        }


        /// TODO: Handle invalid sku remains
        [Theory]
        [InlineData("5C", "0000", 2)]
        public async Task Test_Inalid_sku_remains(string warehouse, string sku, int quantity)
        {
            // Prepare
            // Pre-validate
            // Perform
            // Post-validate
        }

        /// TODO: Handle invalid warehouse remains
        [Theory]
        [InlineData("XX", "0141", 2)]
        public async Task Test_Inalid_warehouse_remains(string warehouse, string sku, int quantity)
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
