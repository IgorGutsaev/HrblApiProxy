using Filuet.Infrastructure.Abstractions.Enums;
using ProxySDK;

namespace Filuet.Hrbl.Ordering.ProxySDK.Test
{
    public class LocationTest
    {
        [Theory]
        [InlineData(Country.Vietnam, "https://hrblproxy.azurewebsites.net/")]
        public async Task Test_Get_Dual_month_status(Country country, string baseUrl)
        {
            // prepare
            HrblOrderingProxyClient client = new HrblOrderingProxyClient(baseUrl);

            // pre-validate
            Assert.NotNull(client);

            // perform
            bool result = await client.GetDualMonthStatusAsync(country);

            // post-validate
            Assert.NotNull(result);
        }
    }
}
