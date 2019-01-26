using System;
using KinguilaAppApi.Models;
using KinguilaAppApi.Services;
using Xunit;

namespace KinguilaAppApi.UnitTests.Services.KinguilaHojeTextualInformationParserTests
{
    public class ParseCurrencyRateShould
    {
        [Theory]
        [InlineData("$ 300", 300, "usd")]
        [InlineData("$ 263.2", 263.2, "usd")]
        [InlineData("€ 100", 100, "eur")]
        [InlineData("€ 125.23", 125.23, "eur")]
        [InlineData("€    125.23", 125.23, "eur")]
        public void ParseCurrencyGivenCorrectInput(String input, decimal expectedAmount, 
            string expectedCurrency)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();

            CurrencyRate currencyRate = parser.ParseCurrencyRate(input);
            
            Assert.NotNull(currencyRate);
            Assert.Equal(expectedAmount, currencyRate.Amount);
            Assert.Equal(Currency.FromISO4217Code(expectedCurrency), currencyRate.Currency);
        }

        [Theory]
        [InlineData("")]
        [InlineData("€")]
        [InlineData("$ 100 12")]
        public void ThrowArgumentExceptionGivenInputWithInvalidTokensCount(string input)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();

            ArgumentException exception = Assert.Throws<ArgumentException>(() => parser.ParseCurrencyRate(input));

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("token count", exception.Message);
        }

        [Theory]
        [InlineData("% 100")]
        [InlineData("12 100")]
        [InlineData("! 100")]
        public void ThrowArgumentExceptionGivenInputWithInvalidCurrencySymbol(string input)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();
            
            ArgumentException exception = Assert.Throws<ArgumentException>(() => parser.ParseCurrencyRate(input));

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("currency symbol", exception.Message);
        }

        [Theory]
        [InlineData("$ -100")]
        [InlineData("€ -120")]
        [InlineData("€ 0")]
        public void ThrowArgumentOutOfRangeExceptionGivenInputWithNonPositiveAmount(string input)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();
            
            ArgumentOutOfRangeException exception = Assert.Throws<ArgumentOutOfRangeException>(() => 
                parser.ParseCurrencyRate(input));

            Assert.Contains("currencyRate", exception.ParamName);
            Assert.Contains("must be positive", exception.Message);
        }

        public KinguilaHojeTextualInformationParser GetDefaultParser()
        {
            return new KinguilaHojeTextualInformationParser(DateProvider.Default());
        }
    }
}