using Filuet.Hrbl.Ordering.Abstractions;
using Newtonsoft.Json;
using System;
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



            string lol = "[{\"Sku\":{\"Name\":\"5438\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2561\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2562\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0003\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0765\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0766\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0767\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0770\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0771\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0830\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0827\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0772\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0043\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"003A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"003U\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"004A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0006\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0020\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0105\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0106\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0124\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"297A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"305A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0155\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"180A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"236A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0242\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"302A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"314A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"1065\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"1800\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"1819\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2563\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2564\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2565\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2566\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"310A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"311A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0773\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"5451\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"306A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R809\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R909\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N305\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N316\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"5443\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"6240\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"6241\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R910\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"2864\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"3143\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"375A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"7640\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"8098\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"8573\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"8705\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"8710\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"8711\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"6999\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"6597\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"6473\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N309\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"7051\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"9985\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"9984\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N315\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N304\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"5001\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N303\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N314\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"5411\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N302\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N313\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N133\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N135\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R812\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R912\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"5652\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"R811\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N311\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N337\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N343\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N345\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N338\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N339\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N344\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N346\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"031U\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"3114\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N741\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N740\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N739\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0260\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4462\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4464\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N977\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N978\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N979\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"N980\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"020N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"021N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4463\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4467\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4466\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"4468\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"411N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"289N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"419N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"423N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"096K\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"670N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"671N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"672N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"749N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"719A\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"794N/ 795N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"814N/ 815N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"820N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"822N/ 823N/ 824N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"836N/ 837N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"843N/ 844N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"984N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"986N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"985N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"459N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"850N/ 851N/ 852N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"854N/ 855N\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":10},{\"Sku\":{\"Name\":\"3150\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"101M\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"0050\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"1437\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5},{\"Sku\":{\"Name\":\"100M\",\"Warehouse\":\"LV\"},\"Count\":1,\"ProductType\":5}]";
            SSSSItem[] x = JsonConvert.DeserializeObject<SSSSItem[]>(lol);
            SkuInventory[] result1 = await _adapter.GetSkuAvailability("LV", x.ToDictionary(x=>x.Sku.Name, y => y.Count));

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
