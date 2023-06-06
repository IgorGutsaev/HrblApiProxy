using System.Net;
using System.Text;
using System.Text.Json;

namespace Filuet.Hrbl.Ordering.Proxy.Helpers
{
    public static class HttpHelpers
    {       
        public static void SendHttpGetUnpromisedRequest(string? uri, string relativeUri)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(new Uri(uri), relativeUri));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "Get";
            //     httpWebRequest.Headers.Add("Authorization", $"Bearer {BearerToken}");
            httpWebRequest.GetResponseAsync();
        }

        public static void SendHttpPostUnpromisedRequest(string? uri, string relativeUri, dynamic payload)
        {
            if (string.IsNullOrWhiteSpace(uri))
                return;

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(new Uri(new Uri(uri), relativeUri));
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "Post";
            //     httpWebRequest.Headers.Add("Authorization", $"Bearer {BearerToken}");

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(JsonSerializer.Serialize(payload));
            }


            httpWebRequest.GetResponseAsync();
        }
    }
}