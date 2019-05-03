using LuKaSo.Mintos.Models.Loans;

namespace LuKaSo.Mintos.Models.Investor
{
    public class LoanCategory
    {
        /// <summary>
        /// Payment status
        /// </summary>
        public LoanStatus PaymentStatus { get; set; }

        /// <summary>
        /// Participation count
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Alocated amount
        /// </summary>
        public decimal Amount { get; set; }
    }
}
