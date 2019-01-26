using System;
using KinguilaAppApi.Models;
using KinguilaAppApi.SharedKernel;
using Xunit;

namespace KinguilaAppApi.UnitTests.ViewModels
{
    public class CurrencyRateTests
    {
        [Theory]
        [InlineData("$ 300", 300, "usd")]
        [InlineData("$ 263.2", 263.2, "usd")]
        [InlineData("€ 100", 100, "eur")]
        [InlineData("€ 125.23", 125.23, "eur")]
        [InlineData("€    125.23", 125.23, "eur")]
        public void ParseShouldParseCurrencyGivenCorrectInput(String input, decimal expectedAmount, 
            string expectedCurrency)
        {
            CurrencyRate currencyRate = CurrencyRate.Parse(input);
            
            Assert.Equal(expectedAmount, currencyRate.Amount);
            Assert.Equal(Currency.FromAlphabeticalCode(expectedCurrency), currencyRate.Currency);
        }

        [Theory]
        [InlineData("")]
        [InlineData("€")]
        [InlineData("$ 100 12")]
        public void ParseShouldThrowArgumentExceptionGivenInputWithInvalidTokensCount(string input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                CurrencyRate.Parse(input);
            });

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("token count", exception.Message);
        }

        [Theory]
        [InlineData("% 100")]
        [InlineData("12 100")]
        [InlineData("! 100")]
        public void ParseShouldThrowArgumentExceptionGivenInputWithInvalidCurrencySymbol(string input)
        {
            ArgumentException exception = Assert.Throws<ArgumentException>(() =>
            {
                CurrencyRate.Parse(input);
            });

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("currency symbol", exception.Message);
        }

        [Theory]
        [InlineData("$ -100")]
        [InlineData("€ -120")]
        [InlineData("€ 0")]
        public void ParseShouldThrowArgumentOutOfRangeExceptionGivenInputWithNonPositiveAmount(string input)
        {
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                CurrencyRate.Parse(input);
            });

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("must be positive", exception.Message);
        }
    }
}