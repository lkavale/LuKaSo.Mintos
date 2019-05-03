using LuKaSo.Mintos.Models.Investor;
using LuKaSo.Mintos.Models.Login;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LuKaSo.Mintos.Api
{
    public interface IMintosApi
    {
        /// <summary>
        /// Get CSRF token
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<string> GetCsrfTokenAsync(CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Login user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="csrfToken"></param>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task LoginAsync(User user, string csrfToken, CancellationToken ct = default(CancellationToken));

        /// <summary>
        /// Get investor overview
        /// </summary>
        /// <param name="ct"></param>
        /// <returns></returns>
        Task<IEnumerable<InvestorOverview>> GetInvestorOverviewAsync(CancellationToken ct = default(CancellationToken));
    }
}
