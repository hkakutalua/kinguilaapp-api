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
        
        public static CurrencyRate Parse(string currencyRate)
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

            return new CurrencyRate(ParseCurrency(currencySymbol), amount);
        }

        private static bool IsValidCurrency(string currencySymbol)
        {
            return _stringToCurrenciesDictionary.Any(keyValuePair => keyValuePair.Key.Equals(currencySymbol));
        }

        private static Currency ParseCurrency(string currencySymbol)
        {
            if (!IsValidCurrency(currencySymbol))
                throw new ArgumentException(currencySymbol);

            return _stringToCurrenciesDictionary[currencySymbol];
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