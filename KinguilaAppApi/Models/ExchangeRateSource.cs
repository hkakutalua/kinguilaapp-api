using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.SharedKernel;

namespace KinguilaAppApi.Models
{
    public class ExchangeRateSource : Enumeration
    {
        public static readonly ExchangeRateSource Official = new ExchangeRateSource(1, "official");
        public static readonly ExchangeRateSource Kinguila = new ExchangeRateSource(2, "kinguila");
        
        public string SourceName { get; }
        
        private ExchangeRateSource(int id, string sourceName)
            : base(id, sourceName)
        {
            if (string.IsNullOrWhiteSpace(sourceName))
                throw new ArgumentException(nameof(sourceName));

            SourceName = sourceName;
        }

        public static IEnumerable<ExchangeRateSource> GetAll() => Enumeration.GetAll<ExchangeRateSource>();

        public bool Equals(string source)
        {
            if (string.IsNullOrWhiteSpace(source))
                throw new ArgumentException(nameof(source));

            return SourceName.ToLowerInvariant().Equals(source.ToLowerInvariant());
        }
    }
}