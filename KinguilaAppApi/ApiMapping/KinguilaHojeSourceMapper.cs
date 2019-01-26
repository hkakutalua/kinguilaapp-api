using System.Collections.Generic;
using KinguilaAppApi.Models;

namespace KinguilaAppApi.ApiMapping
{
    public class KinguilaHojeSourceMapper
    {
        private readonly Dictionary<string, ExchangeRateSource> _kinguilaSourceToModelDictionary
            = new Dictionary<string, ExchangeRateSource>
            {
                { "BNA".ToLowerInvariant(), ExchangeRateSource.Official },
                { "Kinguila Hoje".ToLowerInvariant(), ExchangeRateSource.Kinguila }
            };

        public ExchangeRateSource Map(string kinguilaSourceText) =>
            _kinguilaSourceToModelDictionary[kinguilaSourceText.ToLowerInvariant()];

        public bool CanMap(string kinguilaSourceText) =>
            _kinguilaSourceToModelDictionary.ContainsKey(kinguilaSourceText.ToLowerInvariant());
    }
}