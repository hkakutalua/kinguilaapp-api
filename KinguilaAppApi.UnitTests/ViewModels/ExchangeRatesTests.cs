using System;
using System.Collections.Generic;
using KinguilaAppApi.Models;
using KinguilaAppApi.Services;
using KinguilaAppApi.SharedKernel;
using Moq;
using Xunit;

namespace KinguilaAppApi.UnitTests.ViewModels
{
    public class ExchangeRatesTests
    {
        private readonly Mock<IDateProvider> _dateProviderStub;
        
        public ExchangeRatesTests()
        {
            _dateProviderStub = new Mock<IDateProvider>();
            _dateProviderStub
                .Setup(x => x.GetCurrentDate())
                .Returns(new DateTime(2013, 9, 23));
        }
        
        [Theory]
        [InlineData("BNA", "Actualizado em 21/Janeiro", ExchangeRateSource.Official, "2013-01-21")]
        [InlineData("Kinguila Hoje", "Actualizado em 14/Outubro", ExchangeRateSource.Kinguila, "2013-10-14")]
        public void ParseShouldParseExchangeRatesGivenCorrectInputs(string sourceInput, string dateInput,
            ExchangeRateSource expectedExchangeRateSource, string expectedDate)
        {
            Mock<IDateProvider> dateProviderStub = new Mock<IDateProvider>();
            dateProviderStub
                .Setup(x => x.GetCurrentDate())
                .Returns(DateTimeOffset.Parse(expectedDate));
            
            ExchangeRates exchangeRates = ExchangeRates.Parse(sourceInput, dateInput, GetCurrencyRates(),
                dateProviderStub.Object);
            
            Assert.Equal(expectedExchangeRateSource, exchangeRates.Source);
            Assert.Equal(DateTime.Parse(expectedDate), exchangeRates.UpdatedAt);
            Assert.Equal(GetCurrencyRates(), exchangeRates.CurrencyRates);
        }

        [Theory]
        [InlineData("NBA", "Actualizado em 21/Janeiro")]
        [InlineData("Random", "Actualizado em 21/Janeiro")]
        [InlineData("Boss", "Actualizado em 21/Janeiro")]
        [InlineData("Lorem ipsum", "Actualizado em 21/Janeiro")]
        public void ParseShouldThrowArgumentExceptionGivenUnsupportedSourceInput(string sourceInput, string dateInput)
        {
            var exception = Assert.Throws<ArgumentException>(() => 
                ExchangeRates.Parse(sourceInput, dateInput, GetCurrencyRates(), _dateProviderStub.Object));
            
            Assert.Equal("source", exception.ParamName);
            Assert.Contains("is invalid", exception.Message);
        }
        
        [Theory]
        [InlineData("BNA", "21/Janeiro")]
        [InlineData("Kinguila Hoje", "2013-09-23")]
        [InlineData("BNA", "23-09-2013")]
        [InlineData("Kinguila Hoje", "2013")]
        [InlineData("BNA", "Janeiro")]
        [InlineData("Kinguila Hoje", "21")]
        public void ParseShouldThrowArgumentExceptionGivenUnsupportedDateInput(string sourceInput, string dateInput)
        {
            var exception = Assert.Throws<ArgumentException>(() => 
                ExchangeRates.Parse(sourceInput, dateInput, GetCurrencyRates(), _dateProviderStub.Object));
            
            Assert.Equal("updatedAt", exception.ParamName);
            Assert.Contains("is invalid", exception.Message);
        }

        public static IEnumerable<CurrencyRate> GetCurrencyRates()
        {
            return new List<CurrencyRate>
            {
                new CurrencyRate(Currency.Euro, 1200),
                new CurrencyRate(Currency.UnitedStatesDollar, 300.2m),
                new CurrencyRate(Currency.UnitedStatesDollar, 100),
            };
        }
    }
}