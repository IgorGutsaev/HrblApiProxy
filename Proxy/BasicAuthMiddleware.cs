using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Filuet.Hrbl.Ordering.Proxy
{
    public class BasicAuthMiddleware
    {
        private readonly RequestDelegate _next;

        public BasicAuthMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Make sure we are not hitting it locally
            if (!IsLocalRequest(context))
            {
                string authHeader = context.Request.Headers["Authorization"];
                if (authHeader != null && authHeader.StartsWith("Basic "))
                {
                    // Get the encoded username and password
                    var encodedUsernamePassword = authHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();

                    // Decode from Base64 to string
                    var decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));

                    // Split username and password
                    var username = decodedUsernamePassword.Split(':', 2)[0];
                    var password = decodedUsernamePassword.Split(':', 2)[1];

                    // Check if login is correct
                    if (IsAuthorized(username, password, $"{context.Request.Scheme}://{context.Request.Host.ToUriComponent()}"))
                    {
                        await _next.Invoke(context);
                        return;
                    }
                }

                // Return authentication type (causes browser to show login dialog)
                context.Response.Headers["WWW-Authenticate"] = "Basic";

                // Return unauthorized
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else await _next.Invoke(context);
        }

        public bool IsAuthorized(string username, string password, string address)
        {
            if (!string.Equals(username, "hrblproxy", StringComparison.InvariantCultureIgnoreCase) ||
             !string.Equals(password, "p0sjd1", StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }

        public bool IsLocalRequest(HttpContext context)
        {
            //Handle running using the Microsoft.AspNetCore.TestHost and the site being run entirely locally in memory without an actual TCP / IP connection
            if (context.Connection.RemoteIpAddress == null && context.Connection.LocalIpAddress == null)
                return true;

            if (context.Connection.RemoteIpAddress.Equals(context.Connection.LocalIpAddress))
                return true;

            if (IPAddress.IsLoopback(context.Connection.RemoteIpAddress))
                return true;

            return false;
        }
    }
}