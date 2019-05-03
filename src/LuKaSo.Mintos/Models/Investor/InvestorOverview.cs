using System.Collections.Generic;

namespace LuKaSo.Mintos.Models.Investor
{
    public class InvestorOverview
    {
        /// <summary>
        /// Currency
        /// </summary>
        public Currency Currency { get; set; }

        /// <summary>
        /// Account balance
        /// </summary>
        public AccountBalance AccountBalance { get; set; }

        /// <summary>
        /// Account profit
        /// </summary>
        public AccountProfit AccountProfit { get; set; }

        /// <summary>
        /// Loan categories
        /// </summary>
        public IEnumerable<LoanCategory> LoanCategories { get; set; }
    }
}
