using System;
using KinguilaAppApi.SharedKernel;

namespace KinguilaAppApi.Models
{
    public class Currency : Enumeration
    {
        public static readonly Currency UnitedStatesDollar = new Currency("usd");
        public static readonly Currency Euro = new Currency("eur");
        
        public string Code { get; }

        private Currency(string code)
        {
            Code = code;
        }

        public override string ToString()
        {
            return Code;
        }
        
        public static Currency FromAlphabeticalCode(string alphabeticalCode)
        {
            foreach (var currency in GetAll<Currency>())
            {
                if (currency.Code.Equals(alphabeticalCode))
                    return currency;
            }
            
            throw new ArgumentException($"Currency with code {alphabeticalCode} is not supported", nameof(alphabeticalCode));
        }
    }
}