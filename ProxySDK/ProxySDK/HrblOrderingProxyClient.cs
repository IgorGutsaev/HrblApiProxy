using Filuet.Hrbl.Ordering.Abstractions;
using System.Text;
using System.Text.Json;

namespace ProxySDK
{
    public class HrblOrderingProxyClient
    {
        public HrblOrderingProxyClient(string baseUri)
        {
            _baseUri = baseUri;
        }

        public async Task<SsoAuthResult> GetSsoProfileAsync(string login, string password, bool force)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/herbalife/profile");
                request.Content = new StringContent($"{{ \"login\": \"{login}\", \"password\": \"{password}\", \"force\": {force.ToString().ToLower()} }}", Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<SsoAuthResult>(resultStr);
            }
        }


        private readonly string _baseUri;
    }
}