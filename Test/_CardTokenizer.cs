using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace Filuet.Hrbl.Ordering.Test
{
    internal static class CardTokenizer
    {
        public static string Tokenize(string cardNumber, string uri, string login, string password)
        {
            cardNumber = cardNumber.Replace(" ", "");
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            ServicePointManager.ServerCertificateValidationCallback +=
                (sender, cert, chain, sslPolicyErrors) => true;
            var handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.ServerCertificateCustomValidationCallback =
                (httpRequestMessage, cert, cetChain, policyErrors) =>
                {
                    return true;
                };
            var authValue = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{login}:{password}")));
            var client = new HttpClient(handler)
            {
                DefaultRequestHeaders = { Authorization = authValue }
            };
            string strData = JsonConvert.SerializeObject(new CardTokenizationData { CardNumber = cardNumber });
            var resp = client.PostAsync(uri, new StringContent(strData, Encoding.UTF8, "application/json")).Result;
            var respData = JsonConvert.DeserializeObject<CardTokenizationData>(resp.Content.ReadAsStringAsync().Result);
            return respData.CardNumber;
        }

        private class CardTokenizationData
        {
            /// <summary>
            /// Original or tokenized card number
            /// </summary>
            [JsonProperty(PropertyName = "data")]
            public string CardNumber { get; set; }
        }
    }
}
