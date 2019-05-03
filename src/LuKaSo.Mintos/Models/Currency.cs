using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace LuKaSo.Mintos.Models
{
    public class Currency
    {
        /// <summary>
        /// Currency symbol
        /// </summary>
        public string Symbol { get; set; }

        /// <summary>
        /// Currency abbreviation
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///  ISO code
        /// </summary>
        public int IsoCode { get; set; }

        /// <summary>
        /// Currency name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// DKK
        /// </summary>
        public static readonly Currency Dkk =
            new Currency()
            {
                Symbol = "kr",
                Code = "DKK",
                IsoCode = 208,
                Name = "Dánská koruna"
            };

        /// <summary>
        /// CZK
        /// </summary>
        public static readonly Currency Czk =
            new Currency()
            {
                Symbol = "Kč",
                Code = "CZK",
                IsoCode = 203,
                Name = "Česká koruna"
            };

        /// <summary>
        /// EUR
        /// </summary>
        public static readonly Currency Eur =
            new Currency()
            {
                Symbol = "€",
                Code = "EUR",
                IsoCode = 978,
                Name = "Euro"
            };

        /// <summary>
        /// GBP
        /// </summary>
        public static readonly Currency Gbp =
            new Currency()
            {
                Symbol = "£",
                Code = "GBP",
                IsoCode = 826,
                Name = "Britská libra"
            };

        /// <summary>
        /// GEL
        /// </summary>
        public static readonly Currency Gel =
            new Currency()
            {
                Symbol = "ლ",
                Code = "GEL",
                IsoCode = 981,
                Name = "Gruzínské lari"
            };

        /// <summary>
        /// KZT
        /// </summary>
        public static readonly Currency Kzt =
            new Currency()
            {
                Symbol = "₸",
                Code = "KZT",
                IsoCode = 398,
                Name = "Kazachstánské tenge"
            };

        /// <summary>
        /// MXN
        /// </summary>
        public static readonly Currency Mxn =
            new Currency()
            {
                Symbol = "Mex",
                Code = "MXN",
                IsoCode = 484,
                Name = "Mexické peso"
            };

        /// <summary>
        /// PLN
        /// </summary>
        public static readonly Currency Pln =
            new Currency()
            {
                Symbol = "zł",
                Code = "PLN",
                IsoCode = 985,
                Name = "Polský zlotý"
            };

        /// <summary>
        /// RON
        /// </summary>
        public static readonly Currency Ron =
            new Currency()
            {
                Symbol = "lei",
                Code = "RON",
                IsoCode = 946,
                Name = "Rumunské leu"
            };

        /// <summary>
        /// RUB
        /// </summary>
        public static readonly Currency Rub =
            new Currency()
            {
                Symbol = "₽",
                Code = "RUB",
                IsoCode = 643,
                Name = "Ruský rubl"
            };

        /// <summary>
        /// SEK
        /// </summary>
        public static readonly Currency Sek =
            new Currency()
            {
                Symbol = "kr",
                Code = "SEK",
                IsoCode = 752,
                Name = "Švédská koruna"
            };

        /// <summary>
        /// USD
        /// </summary>
        public static readonly Currency Usd =
            new Currency()
            {
                Symbol = "$",
                Code = "USD",
                IsoCode = 840,
                Name = "Americký dolar"
            };

        /// <summary>
        /// All available currencies
        /// </summary>
        public static readonly IReadOnlyList<Currency> Currencies =
            new ReadOnlyCollection<Currency>(
                new List<Currency>()
                {
                    Czk,
                    Dkk,
                    Eur,
                    Gbp,
                    Gel,
                    Kzt,
                    Mxn,
                    Pln,
                    Ron,
                    Rub,
                    Sek,
                    Usd
                });
    }
}
