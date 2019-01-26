using System;
using System.Collections.Generic;
using KinguilaAppApi.Converters;
using Newtonsoft.Json;

namespace KinguilaAppApi.ViewModels
{
    public class ExchangeRatesViewModel
    {
        public string Source { get; private set; }
        
        [JsonConverter(typeof(OnlyDateConverter))]
        public DateTime UpdatedAt { get; private set; }
        
        public IEnumerable<CurrencyRateViewModel> CurrencyRates { get; private set; }

        public ExchangeRatesViewModel(string source, DateTime updatedAt, IEnumerable<CurrencyRateViewModel> currencyRates)
        {
            Source = source;
            UpdatedAt = updatedAt;
            CurrencyRates = currencyRates;
        }
        
        private ExchangeRatesViewModel() {}
    }
}