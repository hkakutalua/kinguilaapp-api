using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.SharedKernel;

namespace KinguilaAppApi.Models
{
    public class Currency : Enumeration
    {
        public static readonly Currency UnitedStatesDollar = new Currency(1, "usd", "$");
        public static readonly Currency Euro = new Currency(2, "eur", "€");
        
        public string Code { get; }
        public string Symbol { get; }

        private Currency(int id, string code, string symbol)
            : base(id, code)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException(nameof(code));
            if (string.IsNullOrWhiteSpace(symbol))
                throw new ArgumentException(nameof(symbol));
            
            Code = code;
            Symbol = symbol;
        }

        public override string ToString()
        {
            return Code;
        }
        
        public static Currency FromISO4217Code(string alphabeticalCode)
        {
            foreach (var currency in GetAll<Currency>())
            {
                if (currency.Code.Equals(alphabeticalCode))
                    return currency;
            }
            
            throw new ArgumentException($"Currency with code {alphabeticalCode} is not supported", nameof(alphabeticalCode));
        }

        public static IEnumerable<Currency> FromSymbol(string currencySymbol)
        {
            currencySymbol = currencySymbol.Trim();
            
            foreach (Currency currency in GetAll<Currency>())
            {
                if (currency.Symbol.Equals(currencySymbol))
                    yield return currency;
            }
        }
    }
}