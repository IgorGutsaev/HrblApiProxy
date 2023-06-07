using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.ProxySDK.Models;
using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using System.Text;
using System.Text.Json;

namespace ProxySDK
{
    public class HrblOrderingProxyClient
    {
        public HrblOrderingProxyClient(string baseUri, string login, string password)
        {
            _baseUri = baseUri;
            _login = login;
            _password = password;

            _getSsoProfileJsonSerializationOptions = new JsonSerializerOptions();
            _getSsoProfileJsonSerializationOptions.Converters.Add(new CountryJsonConverter());
        }

        public async Task<SsoAuthResult> GetSsoProfileAsync(string login, string password, bool force, Country? country)
        {
            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Invalid credentials");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/herbalife/ssoprofile");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                AuthCredentials payload = new AuthCredentials { Login = login, Password = password, Force = force, Country = country };

                request.Content = new StringContent(JsonSerializer.Serialize(payload, _getSsoProfileJsonSerializationOptions), Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<SsoAuthResult>(resultStr);
            }
        }

        public async Task<DistributorProfile> GetProfileAsync(string memberId)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("Invalid distributor id");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/herbalife/profile/{memberId}");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<DistributorProfile>(resultStr);
            }
        }

        public async Task<DistributorVolumePoints[]> GetVolumePointsAsync(string memberId, DateTime month, DateTime? monthTo)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("Invalid distributor id");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/herbalife/profile/vp");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                VPRequest payload = new VPRequest { MemberId = memberId, Month = month, MonthTo = monthTo };
                request.Content = new StringContent(JsonSerializer.Serialize(payload, _getSsoProfileJsonSerializationOptions), Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<DistributorVolumePoints[]>(resultStr);
            }
        }

        public async Task<FOPPurchasingLimitsResult> GetDSFOPLimitsAsync(string memberId, Country country)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("Invalid distributor id");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/herbalife/profile/fop");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                MemberCountryRequest payload = new MemberCountryRequest { MemberId = memberId, Country = country };
                request.Content = new StringContent(JsonSerializer.Serialize(payload, _getSsoProfileJsonSerializationOptions), Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<FOPPurchasingLimitsResult>(resultStr);
            }
        }

        public async Task<TinDetails> GetTinAsync(string memberId, Country country)
        {
            if (string.IsNullOrWhiteSpace(memberId))
                throw new ArgumentException("Invalid distributor id");

            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Post, "/api/herbalife/profile/tin");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                MemberCountryRequest payload = new MemberCountryRequest { MemberId = memberId, Country = country };
                request.Content = new StringContent(JsonSerializer.Serialize(payload, _getSsoProfileJsonSerializationOptions), Encoding.Default, "application/json");

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return JsonSerializer.Deserialize<TinDetails>(resultStr);
            }
        }

        public async Task<bool> GetDualMonthStatusAsync(Country country)
        {
            using (HttpClient httpClient = new HttpClient())
            {
                httpClient.BaseAddress = new Uri(_baseUri);
                var request = new HttpRequestMessage(HttpMethod.Get, $"/api/herbalife/dualmonth/{country.GetCode().ToLower()}");
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes($"{_login}:{_password}")));

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return Boolean.Parse(resultStr);
            }
        }

        private readonly JsonSerializerOptions _getSsoProfileJsonSerializationOptions;
        private readonly string _baseUri;
        private readonly string _login;
        private readonly string _password;
    }
}