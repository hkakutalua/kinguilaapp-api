using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.Models;

namespace KinguilaAppApi.Services
{
    public class KinguilaHojeTextualInformationParser : IPageTextualInformationParser
    {
        private static readonly Dictionary<string, int> _monthToNumberInPortuguese =
            new Dictionary<string, int>
            {
                { "Janeiro", 1 },
                { "Fevereiro", 2 },
                { "Março", 3 },
                { "Abril", 4 },
                { "Maio", 5 },
                { "Junho", 6 },
                { "Julho", 7 },
                { "Agosto", 8 },
                { "Setembro", 9 },
                { "Outubro", 10 },
                { "Novembro", 11 },
                { "Dezembro", 12 },
            };

        private readonly IDateProvider _dateProvider;

        public KinguilaHojeTextualInformationParser(IDateProvider dateProvider)
        {
            if (dateProvider == null)
                throw new ArgumentNullException(nameof(dateProvider));

            _dateProvider = dateProvider;
        }
        
        /// <summary>
        /// Parses a currency rate string from the format "{currencySymbol} {amount}" to an <see cref="CurrencyRate"/>
        /// <para /> Examples of currency rates strings: "$ 100" "Kz 12000" "€ 50.23" 
        /// </summary>
        /// <param name="currencyRate">The currency rate formatted string</param>
        /// <returns>An <see cref="CurrencyRate"/> object</returns>
        public CurrencyRate ParseCurrencyRate(string currencyRate)
        {
            string[] tokens = currencyRate.Split()
                .Where(x => x != string.Empty)
                .ToArray();
            
            if (tokens.Length != 2)
                throw new ArgumentException($"The {nameof(currencyRate)} token count can't be different of 2", nameof(currencyRate));

            string currencySymbol = tokens[0];
            decimal amount = decimal.Parse(tokens[1]);
            
            if (!IsValidCurrency(currencySymbol))
                throw new ArgumentException("The currency symbol specified is invalid", nameof(currencyRate));
            
            if (amount <= 0)
                throw new ArgumentOutOfRangeException(nameof(currencyRate), "The currency rate amount must be positive");

            Currency currency = Currency.FromSymbol(currencySymbol).First();
            return new CurrencyRate(currency, amount);
        }

        private static bool IsValidCurrency(string currencySymbol) => 
            Currency.FromSymbol(currencySymbol).Count() != 0;

        /// <summary>
        /// Parses a date string from the format "Actualizado em {day}/{monthStringInPortuguese} to an <see cref="DateTimeOffset"/>
        /// <para /> Examples of date strings are: "Actualizado em 23/Setembro" "Actualizado em 4/Fevereiro"
        /// </summary>
        /// <param name="date">The date string in portuguese</param>
        /// <returns>An equivalent <see cref="DateTimeOffset"/> object</returns>
        public DateTimeOffset ParseDate(string date)
        {
            if (!CanParse(date))
                throw new ArgumentException("The specified date is invalid", nameof(date));
                
            string dayAndMonth = date.Replace("Actualizado desde", string.Empty);
            string[] tokens = dayAndMonth.Split("/");

            int day = int.Parse(tokens[0]);
            string monthInPortuguese = tokens[1].Trim();
            int month = _monthToNumberInPortuguese[monthInPortuguese];
            
            return new DateTime(_dateProvider.GetCurrentDate().Year, month, day);
        }

        private bool CanParse(string date)
        {
            if (date.IndexOf("Actualizado desde") != 0)
                return false;
            
            date = date.Replace("Actualizado desde", string.Empty);

            string[] tokens = date.Split("/");
            string day = tokens[0];
            string monthInPortuguese = tokens[1].Trim();

            if (!int.TryParse(day, out int result))
                return false;

            if (!_monthToNumberInPortuguese.ContainsKey(monthInPortuguese))
                return false;

            return true;
        }
    }
}