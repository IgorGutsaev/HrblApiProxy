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
       // [InlineData("RG", "0141", 1)]
        //[InlineData("U7", "0006", 1)]
        //[InlineData("AI", "0006", 1)]
        [InlineData("9O", "0141", 1)]
        public async Task Test_Valid_sku_remains(string warehouse, string sku, int quantity)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(warehouse));
            Assert.False(string.IsNullOrWhiteSpace(sku));
            Assert.True(quantity > 0);



            string lol = "{\"0145\":1,\"0141\":1,\"0142\":1,\"0143\":1,\"0144\":1,\"0146\":1,\"1171\":1,\"2793\":1,\"2653\":1,\"0242\":1,\"2864\":1,\"0006\":1,\"1065\":1,\"2670\":1,\"2669\":1,\"0258\":1,\"0259\":1,\"0260\":1,\"1432\":1,\"1437\":1,\"1466\":1,\"0111\":1,\"0003\":1,\"0022\":1,\"0050\":1,\"0106\":1,\"0155\":1,\"2561\":1,\"2562\":1,\"2563\":1,\"2564\":1,\"2565\":1,\"2566\":1,\"0765\":1,\"0766\":1,\"0767\":1,\"0770\":1,\"0771\":1,\"0772\":1,\"0773\":1,\"0827\":1,\"0828\":1,\"0829\":1,\"0830\":1,\"297A\":1,\"296A\":1,\"100M\":1,\"8697\":1,\"8705\":1,\"I041\":1,\"I042\":1,\"I043\":1,\"I044\":1,\"I045\":1,\"00H5\":1,\"00H6\":1,\"00H7\":1,\"12H1\":1,\"12H2\":1,\"12H3\":1,\"8711\":1,\"8710\":1,\"180A\":1,\"302A\":1,\"299A\":1,\"305A\":1,\"314A\":1,\"311A\":1,\"508H\":1,\"N947\":1,\"N946\":1,\"043K\":1,\"6240\":1,\"6940\":1,\"5776\":1,\"6241\":1,\"N923\":1,\"N992\":1,\"N924\":1,\"N203\":1,\"N204\":1,\"N205\":1,\"5451\":1,\"5617\":1,\"5498\":1,\"5629\":1,\"N153\":1,\"N154\":1,\"N155\":1,\"5001\":1,\"310A\":1,\"309A\":1,\"628A\":1,\"629A\":1,\"7640\":1}";
            Dictionary<string, int> x = JsonConvert.DeserializeObject<Dictionary<string, int>>(lol);
            SkuInventory[] result1 = await _adapter.GetSkuAvailability("LV", x.ToDictionary(x=>x.Key, y => y.Value));

            // Perform
            SkuInventory result = await _adapter.GetSkuAvailability(warehouse, sku, quantity);

            // Post-validate
            Assert.NotNull(result);
            Assert.True(result.Sku == sku);
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
