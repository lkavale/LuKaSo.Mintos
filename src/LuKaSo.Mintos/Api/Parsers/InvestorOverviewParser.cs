using HtmlAgilityPack;
using LuKaSo.Mintos.Models;
using LuKaSo.Mintos.Models.Investor;
using LuKaSo.Mintos.Models.Loans;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LuKaSo.Mintos.Api.Parsers
{
    public class InvestorOverviewParser
    {
        /// <summary>
        /// Parse list of investor overviews
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        public IEnumerable<InvestorOverview> Parse(HtmlDocument document)
        {
            var currencyButtons = document.DocumentNode.SelectNodes(".//div[@id='withdraw']/div/button");
            var currencies = currencyButtons.Select(cb => ParserHelpers.ResolveCurrency(cb.InnerText.Trim()));

            return currencies.Select(c =>
            {
                var box = document.DocumentNode.SelectSingleNode($".//ul[@id='mintos-boxes' and contains(@v-bind, '{c.IsoCode}')]");
                return GetBox(box, c);
            });
        }

        /// <summary>
        /// Parse data for one currency
        /// </summary>
        /// <param name="node"></param>
        /// <param name="currency"></param>
        /// <returns></returns>
        private InvestorOverview GetBox(HtmlNode node, Currency currency)
        {
            var boxes = node.SelectNodes(".//li[@class='overview-box']");

            // Resolve first box
            var accountBalance = ResolveBalanceBox(boxes[0]);

            // Resolve second box
            var accountProfit = ResolveProfitBox(boxes[1]);

            // Resolve category box"es"
            var loanCategories = ((LoanStatus[])Enum.GetValues(typeof(LoanStatus)))
                .Select(e => new LoanCategory() { PaymentStatus = e })
                .ToList();

            var categoryBoxes = boxes[2].SelectNodes(".//ul[@class='m-investment-totals']/li");

            ResolveCategoryBox(categoryBoxes[0], loanCategories, (category, text) => category.Amount = ParserHelpers.ParseDecimal(text));
            ResolveCategoryBox(categoryBoxes[1], loanCategories, (category, text) => category.Count = ParserHelpers.ParseInteger(text));

            return new InvestorOverview()
            {
                Currency = currency,
                AccountBalance = accountBalance,
                AccountProfit = accountProfit,
                LoanCategories = loanCategories
            };
        }

        /// <summary>
        /// Parse data from account balance box
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AccountBalance ResolveBalanceBox(HtmlNode node)
        {
            var accountBalance = new AccountBalance();

            foreach (var tr in node.SelectNodes(".//table[@class='data']/tr"))
            {
                var td = tr.SelectNodes(".//td");

                switch (td[0].InnerText.Trim())
                {
                    case var name when name.Equals("Available Funds", StringComparison.InvariantCultureIgnoreCase):
                        accountBalance.AvailableFunds = ParserHelpers.ParseDecimal(td[1].InnerText);
                        break;
                    case var name when name.Equals("Invested Funds", StringComparison.InvariantCultureIgnoreCase):
                        accountBalance.InvestedFunds = ParserHelpers.ParseDecimal(td[1].InnerText);
                        break;
                }
            }

            return accountBalance;
        }

        /// <summary>
        /// Parse data from account profit box
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private AccountProfit ResolveProfitBox(HtmlNode node)
        {
            var accountProfit = new AccountProfit();

            foreach (var row in node.SelectNodes(".//table[@class='data']/tr"))
            {
                var cols = row.SelectNodes(".//td");

                switch (cols[0].InnerText.Trim())
                {
                    case var name when name.Equals("Interest", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.Interest = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                    case var name when name.Equals("Late Payment Fees", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.LatePaymentFees = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                    case var name when name.Equals("Bad Debt", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.BadDebt = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                    case var name when name.Equals("Secondary Market Transactions", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.SecondaryMarketTransactions = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                    case var name when name.Equals("Service Fees", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.ServiceFees = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                    case var name when name.Equals("Campaign Rewards", StringComparison.InvariantCultureIgnoreCase):
                        accountProfit.CampaignRewards = ParserHelpers.ParseDecimal(cols[1].InnerText);
                        break;
                }
            }

            return accountProfit;
        }

        /// <summary>
        /// Parse data from loan category box
        /// </summary>
        /// <param name="node"></param>
        /// <param name="categories"></param>
        /// <param name="action"></param>
        private void ResolveCategoryBox(HtmlNode node, IList<LoanCategory> categories, Action<LoanCategory, string> action)
        {
            foreach (var row in node.SelectNodes(".//table[@class='data']/tr"))
            {
                var cols = row.SelectNodes(".//td");

                if (ParserHelpers.TryParseLoanType(cols[0].InnerText, out var loanStatus))
                {
                    var category = categories.Single(c => c.PaymentStatus == loanStatus);
                    action(category, cols[1].InnerText.Trim());
                }
            }
        }
    }
}
