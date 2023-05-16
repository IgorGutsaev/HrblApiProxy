using Filuet.Hrbl.Ordering.Abstractions;
using ProxySDK;

namespace ProxySDKTest
{
    public class AuthTest
    {
        [Theory]
        [InlineData("Trangle1967", "Trangle1967", "https://hrblproxy.azurewebsites.net/", false)]
        public async Task Test_SSO_Auth(string login, string password, string baseUrl, bool force)
        {
            // prepare
            HrblOrderingProxyClient client = new HrblOrderingProxyClient(baseUrl);

            // pre-validate
            Assert.NotNull(client);

            // perform
            SsoAuthResult result = await client.GetSsoProfileAsync(login, password, force);

            // post-validate
            Assert.NotNull(result);
        }
    }
}