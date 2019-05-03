using HtmlAgilityPack;
using LuKaSo.Mintos.Api.Parsers;
using LuKaSo.Mintos.Exceptions;
using LuKaSo.Mintos.Extensions;
using LuKaSo.Mintos.Models.Investor;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Mintos.Api
{
    public partial class MintosApi
    {
        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        public async Task<IEnumerable<InvestorOverview>> GetInvestorOverviewAsync(CancellationToken ct = default(CancellationToken))
        {
            using (var request = new HttpRequestMessage())
            {
                request.RequestUri = _baseUrl
                    .Append("en/overview");

                request.Method = new HttpMethod("GET");

                using (var response = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, ct).ConfigureAwait(false))
                using (var responseStream = await response.Content.ReadAsStreamAsync())
                {
                    if (response.RequestMessage.RequestUri == _baseUrl.Append("/en/overview/"))
                    {
                        var overviewPage = new HtmlDocument();
                        overviewPage.Load(responseStream);

                        return new InvestorOverviewParser().Parse(overviewPage);
                    }

                    throw new ServerErrorException(response);
                }
            }
        }
    }
}
