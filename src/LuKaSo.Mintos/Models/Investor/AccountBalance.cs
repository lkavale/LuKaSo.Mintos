namespace LuKaSo.Mintos.Models.Investor
{
    public class AccountBalance
    {
        /// <summary>
        /// Available funds in currency
        /// </summary>
        public decimal AvailableFunds { get; set; }

        /// <summary>
        /// Invested funds in currency
        /// </summary>
        public decimal InvestedFunds { get; set; }
    }
}
