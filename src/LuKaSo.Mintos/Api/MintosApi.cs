using HtmlAgilityPack;
using LuKaSo.Mintos.Exceptions;
using LuKaSo.Mintos.Extensions;
using LuKaSo.Mintos.Logging;
using LuKaSo.Mintos.Models.Login;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Mintos.Api
{
    public partial class MintosApi : IMintosApi, IDisposable
    {
        /// <summary>
        /// Mintos API base address
        /// </summary>
        private readonly Uri _baseUrl;

        /// <summary>
        /// Used HTTP client
        /// </summary>
        private HttpClient _httpClient;

        /// <summary>
        /// Log
        /// </summary>
        private readonly ILog _log;

        /// <summary>
        /// Mintos API constructor with default production address of service
        /// </summary>
        /// <param name="httpClient">HTTP client</param>
        public MintosApi(HttpClient httpClient) : this(new Uri("https://www.mintos.com/"), httpClient)
        {
        }

        /// <summary>
        /// Mintos API constructor
        /// </summary>
        /// <param name="baseUrl">Base URL of service</param>
        /// <param name="httpClient">HTTP client</param>
        public MintosApi(Uri baseUrl, HttpClient httpClient)
        {
            _httpClient = httpClient;
            _baseUrl = baseUrl;

            _log = LogProvider.For<MintosApi>();
        }

        /// <summary>
        /// Mintos API destructor
        /// </summary>
        ~MintosApi()
        {
            Dispose(false);
        }

        /// <summary>
        /// Get CSRF token
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<string> GetCsrfTokenAsync(CancellationToken ct = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("en/login");
                request.Method = new HttpMethod("GET");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    var loginPage = new HtmlDocument();
                    loginPage.Load(responseStream);

                    return loginPage.DocumentNode
                        .SelectSingleNode("//*[@id='login-form']/input[@name='_csrf_token']")
                        .GetAttributeValue("value", "");
                }
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="csrfToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task LoginAsync(User user, string csrfToken, CancellationToken ct = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl.Append("en/login/check");
                request.Content = new FormUrlEncodedContent(new Dictionary<string, string>() {
                     { "_csrf_token", csrfToken },
                     { "_username", user.Name },
                     { "_password", user.Password } });
                request.Method = new HttpMethod("POST");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new ServerErrorException(response);
                    }

                    if(response.RequestMessage.RequestUri == _baseUrl.Append("/en/overview/"))
                    { 
                        var loginPage = new HtmlDocument();
                        loginPage.Load(responseStream);
                        return;
                    }

                    throw new BadLoginException(response, user);
                }
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                _httpClient.Dispose();
            }
        }
    }
}
