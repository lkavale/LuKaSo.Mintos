namespace LuKaSo.Mintos.Models.Investor
{
    public class AccountProfit
    {
        /// <summary>
        /// Profit from interest
        /// </summary>
        public decimal Interest { get; set; }

        /// <summary>
        /// Profit from late payment fees
        /// </summary>
        public decimal LatePaymentFees { get; set; }

        /// <summary>
        /// Bad debt P/L
        /// </summary>
        public decimal BadDebt { get; set; }

        /// <summary>
        /// Secondary market P/L
        /// </summary>
        public decimal SecondaryMarketTransactions { get; set; }

        /// <summary>
        /// Service fees
        /// </summary>
        public decimal ServiceFees { get; set; }

        /// <summary>
        /// Capaign reward
        /// </summary>
        public decimal CampaignRewards { get; set; }
    }
}
