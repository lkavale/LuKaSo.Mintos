namespace LuKaSo.Mintos.Models.Loans
{
    public enum LoanStatus
    {
        /// <summary>
        /// Current
        /// </summary>
        Current,

        /// <summary>
        /// Grace period
        /// </summary>
        GracePeriod,

        /// <summary>
        /// 1-15 days late
        /// </summary>
        DaysLate1to15,

        /// <summary>
        /// 16-30 days late
        /// </summary>
        DaysLate16to30,

        /// <summary>
        /// 31-60 days late
        /// </summary>
        DaysLate31to60,

        /// <summary>
        /// 60+ days late
        /// </summary>
        DaysLate60plus,

        /// <summary>
        /// Default
        /// </summary>
        Default,

        /// <summary>
        /// Bad debt
        /// </summary>
        BadDebt
    }
}
