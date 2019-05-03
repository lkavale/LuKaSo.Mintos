using LuKaSo.Mintos.Models;
using LuKaSo.Mintos.Models.Loans;
using System;
using System.Globalization;
using System.Linq;

namespace LuKaSo.Mintos.Api.Parsers
{
    public static class ParserHelpers
    {
        /// <summary>
        /// Parse nullable double, remove all chars except [0-9],[.],[,],[-]
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static double? ParseNullableDouble(string numberString)
        {
            var numericString = string.Concat(numberString.Where(ch => char.IsDigit(ch) || ch == '.' || ch == ',' || ch == '-')).Replace(',', '.');

            if (double.TryParse(numericString, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                return number;
            }

            return null;
        }

        /// <summary>
        /// Parse double, remove all chars except [0-9],[.],[,],[-]
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static double ParseDouble(string numberString)
        {
            var numericString = string.Concat(numberString.Where(ch => char.IsDigit(ch) || ch == '.' || ch == ',' || ch == '-')).Replace(',', '.');

            return double.Parse(numericString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parse decimal, remove all chars except [0-9],[.],[,],[-]
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static decimal ParseDecimal(string numberString)
        {
            var numericString = string.Concat(numberString.Where(ch => char.IsDigit(ch) || ch == '.' || ch == ',' || ch == '-')).Replace(',', '.');

            return decimal.Parse(numericString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Parse nullable integer, remove all chars except [0-9],[-]
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static int? ParseNullableInteger(string numberString)
        {
            var numericString = string.Concat(numberString.Where(ch => char.IsDigit(ch) || ch == '-'));

            if (int.TryParse(numericString, NumberStyles.Any, CultureInfo.InvariantCulture, out var number))
            {
                return number;
            }

            return null;
        }

        /// <summary>
        /// Parse integer, remove all chars except [0-9],[-]
        /// </summary>
        /// <param name="numberString"></param>
        /// <returns></returns>
        public static int ParseInteger(string numberString)
        {
            var numericString = string.Concat(numberString.Where(ch => char.IsDigit(ch) || ch == '-'));

            return int.Parse(numericString, CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Get currency from code
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static Currency ResolveCurrency(string code)
        {
            return Currency.Currencies
                .SingleOrDefault(c => c.Code.Equals(code, StringComparison.InvariantCultureIgnoreCase));
        }

        /// <summary>
        /// Parse text to loan status
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryParseLoanType(string text, out LoanStatus loanStatus)
        {
            switch (text.Trim().Replace(' ', default(char)))
            {
                case var name when name.Equals("Current", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.Current;
                    return true;
                case var name when name.Equals("GracePeriod", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.GracePeriod;
                    return true;
                case var name when name.Equals("1-15DaysLate", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.DaysLate1to15;
                    return true;
                case var name when name.Equals("16-30DaysLate", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.DaysLate16to30;
                    return true;
                case var name when name.Equals("31-60DaysLate", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.DaysLate31to60;
                    return true;
                case var name when name.Equals("60+DaysLate", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.DaysLate60plus;
                    return true;
                case var name when name.Equals("Default", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.Default;
                    return true;
                case var name when name.Equals("BadDebt", StringComparison.InvariantCultureIgnoreCase):
                    loanStatus = LoanStatus.BadDebt;
                    return true;
            }

            loanStatus = default(LoanStatus);
            return false;
        }

        /// <summary>
        /// Parse text to loan type
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool TryParseLoanStatus(string text, out LoanType loanType)
        {
            switch (text.Trim().Replace(' ', default(char)))
            {
                case var name when name.Equals("agriculturalloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Agricultural;
                    return true;
                case var name when name.Equals("businessloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Business;
                    return true;
                case var name when name.Equals("carloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Car;
                    return true;
                case var name when name.Equals("invoicefinancing", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.InvoiceFinancing;
                    return true;
                case var name when name.Equals("mortgageloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Mortage;
                    return true;
                case var name when name.Equals("pawnbrokingloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Pawnbroking;
                    return true;
                case var name when name.Equals("personalloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.Personal;
                    return true;
                case var name when name.Equals("short-termloan", StringComparison.InvariantCultureIgnoreCase):
                    loanType = LoanType.ShortTerm;
                    return true;
            }

            loanType = default(LoanType);
            return false;
        }
    }
}
