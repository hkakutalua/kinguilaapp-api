using System;
using System.Collections.Generic;
using System.Linq;
using KinguilaAppApi.SharedKernel;

namespace KinguilaAppApi.Models
{
    public class CurrencyRate : IEquatable<CurrencyRate>
    {
        public Currency Currency { get; }
        public decimal Amount { get; }

        private static readonly Dictionary<string, Currency> _stringToCurrenciesDictionary = 
            new Dictionary<string, Currency>
            {
                { "$", Currency.UnitedStatesDollar },
                { "€", Currency.Euro }
            };
        
        public CurrencyRate(Currency currency, decimal amount)
        {
            Currency = currency;
            Amount = amount;
        }


        public bool Equals(CurrencyRate other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(Currency, other.Currency) && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((CurrencyRate) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((Currency != null ? Currency.GetHashCode() : 0) * 397) ^ Amount.GetHashCode();
            }
        }
    }
}