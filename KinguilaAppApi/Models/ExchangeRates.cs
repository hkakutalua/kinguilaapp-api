using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.Services;

namespace KinguilaAppApi.Models
{
    public class ExchangeRates
    {
        public ExchangeRateSource Source { get; }
        public DateTimeOffset UpdatedAt { get; }
        public IEnumerable<CurrencyRate> CurrencyRates { get; }
        
        public ExchangeRates(ExchangeRateSource source, DateTimeOffset updatedAt, IEnumerable<CurrencyRate> currencyRates)
        {
            Source = source;
            UpdatedAt = updatedAt;
            CurrencyRates = currencyRates ?? throw new ArgumentNullException(nameof(currencyRates));
        }
        
        private ExchangeRates() {}
    }
}