using Filuet.Hrbl.Ordering.Abstractions;
using ProxySDK;

namespace ProxySDKTest
{
    public class AuthTest
    {
        [Theory]
        [InlineData("Trangle1967", "Trangle1967", "https://hrblproxy.azurewebsites.net/", false)]
        [InlineData("40X0006447@testherbalife.com", "test@123", "https://hrblproxy.azurewebsites.net/", false)]
        public async Task Test_SSO_Auth(string login, string password, string baseUrl, bool force)
        {
            // prepare
            HrblOrderingProxyClient client = new HrblOrderingProxyClient(baseUrl);

            // pre-validate
            Assert.NotNull(client);

            // perform
            SsoAuthResult result = await client.GetSsoProfileAsync(login, password, force, Filuet.Infrastructure.Abstractions.Enums.Country.Japan);

            // post-validate
            Assert.NotNull(result);
        }

        [Theory]
        [InlineData("VA00248957", "https://hrblproxy.azurewebsites.net/")]
        [InlineData("40X0006447", "https://hrblproxy.azurewebsites.net/")]
        public async Task Test_Get_Profile(string memberId, string baseUrl)
        {
            // prepare
            HrblOrderingProxyClient client = new HrblOrderingProxyClient(baseUrl);

            // pre-validate
            Assert.NotNull(client);

            // perform
            DistributorProfile result = await client.GetProfileAsync(memberId);

            // post-validate
            Assert.NotNull(result);
        }
    }
}