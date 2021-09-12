using Filuet.Hrbl.Ordering.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Filuet.Hrbl.Ordering.Tests
{
    public class LocationTest : BaseTest
    {
        [Theory]
        [InlineData("my")]
        public async Task Test_Get_ds_fop_purchasing_limits(string country)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(country));

            // Perform
            bool result = await _adapter.GetOrderDualMonthStatus(country);

            // Post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("RU", "Pick Point", "Москва")]
        public async Task Test_Get_Pickup_Points(string country, string postamatType, string city)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(country));
            Assert.False(string.IsNullOrWhiteSpace(postamatType));

            // Perform
            DsPostamatDetails[] result =
                await _adapter.GetPostamats(country, postamatType, city: city);

            // Post-validate
            Assert.True(result.Any());
        }

        [Theory]
        [InlineData("618400", true)]
        [InlineData("618400", false)]
        public async Task Test_Get_WH_and_Freight_codes(string postalCode, bool expressDelivery)
        {
            // Prepare
            Assert.NotNull(_adapter);

            // Pre-validate
            Assert.False(string.IsNullOrWhiteSpace(postalCode));

            // Perform
            WHFreightCode[] result =
                await _adapter.GetShippingWhseAndFreightCodes(postalCode, expressDelivery);

            // Post-validate
            Assert.True(result.Any());
        }
    }
}
