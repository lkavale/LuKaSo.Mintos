using System.Net;
using System.Net.Http;

namespace LuKaSo.Mintos.Tests.Production
{
    public class HttpClientBuilder
    {
        public HttpClient Build()
        {
            var cookieContainer = new CookieContainer();
            var handler = new HttpClientHandler() { CookieContainer = cookieContainer, UseCookies = true, AllowAutoRedirect = true };
            var client = new HttpClient(handler);

            return client;
        }
    }
}
