using Filuet.Hrbl.Ordering.Abstractions;
using Filuet.Hrbl.Ordering.ProxySDK.Models;
using Filuet.Infrastructure.Abstractions.Converters;
using Filuet.Infrastructure.Abstractions.Enums;
using Filuet.Infrastructure.Abstractions.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace ProxySDK
{
    public class HrblOrderingProxyClient
    {
        public HrblOrderingProxyClient(string baseUri)
        {
            _baseUri = baseUri;

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

                HttpResponseMessage response = await httpClient.SendAsync(request);

                string resultStr = response.Content.ReadAsStringAsync().Result;
                return Boolean.Parse(resultStr);
            }
        }

        private readonly JsonSerializerOptions _getSsoProfileJsonSerializationOptions;
        private readonly string _baseUri;
    }
}