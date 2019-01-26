using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.Services;

namespace KinguilaAppApi.Models
{
    public class ExchangeRates
    {
        public ExchangeRateSource Source { get; }
        public DateTime UpdatedAt { get; }
        public IEnumerable<CurrencyRate> CurrencyRates { get; }

        private static Dictionary<string, ExchangeRateSource> StringToRateSource =
            new Dictionary<string, ExchangeRateSource>
            {
                { "bna", ExchangeRateSource.Official },
                { "kinguila hoje", ExchangeRateSource.Kinguila }
            };

        private static Dictionary<string, int> MonthToIntInPortuguese =
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

        public ExchangeRates(ExchangeRateSource source, DateTime updatedAt, IEnumerable<CurrencyRate> currencyRates)
        {
            Source = source;
            UpdatedAt = updatedAt;
            CurrencyRates = currencyRates ?? throw new ArgumentNullException(nameof(currencyRates));
        }
        
        private ExchangeRates() {}

        public static ExchangeRates Parse(string source, string updatedAt, IEnumerable<CurrencyRate> currencyRates,
            IDateProvider dateProvider)
        {
            if (!StringToRateSource.ContainsKey(source.ToLowerInvariant()))
                throw new ArgumentException("The exchange rate source specified is invalid", nameof(source));
            
            if (!CanParseKinguilaHojeDate(updatedAt))
                throw new ArgumentException("The date specified is invalid", nameof(updatedAt));
            
            ExchangeRateSource exchangeRateSource = StringToRateSource[source.ToLowerInvariant()];
            DateTime exchangeUpdatedAt = ParseKinguilaHojeDate(updatedAt, dateProvider);
            
            return new ExchangeRates(exchangeRateSource, exchangeUpdatedAt, currencyRates);
        }

        public static string[] GetExchangeRatesSources()
        {
            return StringToRateSource.Keys.ToArray();
        }

        private static DateTime ParseKinguilaHojeDate(string dateInput, IDateProvider dateProvider)
        {
            if (!CanParseKinguilaHojeDate(dateInput))
                throw new ArgumentException("The specified date is invalid", nameof(dateInput));
                
            string dayAndMonth = dateInput.Replace("Actualizado em", string.Empty);
            string[] tokens = dayAndMonth.Split("/");

            int day = int.Parse(tokens[0]);
            string monthInPortuguese = tokens[1].Trim();
            int month = MonthToIntInPortuguese[monthInPortuguese];
            
            return new DateTime(dateProvider.GetCurrentDate().Year, month, day);
        }
        
        private static bool CanParseKinguilaHojeDate(string date)
        {
            if (date.IndexOf("Actualizado em") != 0)
                return false;
            
            date = date.Replace("Actualizado em", string.Empty);

            string[] tokens = date.Split("/");
            string day = tokens[0];
            string monthInPortuguese = tokens[1].Trim();

            if (!int.TryParse(day, out int result))
                return false;

            if (!MonthToIntInPortuguese.ContainsKey(monthInPortuguese))
                return false;

            return true;
        }
    }
}