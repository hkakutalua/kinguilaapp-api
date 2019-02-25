using System;
using KinguilaAppApi.Services;
using Moq;
using Xunit;

namespace KinguilaAppApi.UnitTests.Services.KinguilaHojeTextualInformationParserTests
{
    public class ParseDateShould
    {
        [Theory]
        [InlineData("Atualizado desde 20/Janeiro", 1, 20)]
        [InlineData("Atualizado desde 20/Fevereiro", 2, 20)]
        [InlineData("Atualizado desde 20/Março", 3, 20)]
        [InlineData("Atualizado desde 20/Abril", 4, 20)]
        [InlineData("Atualizado desde 20/Maio", 5, 20)]
        [InlineData("Atualizado desde 20/Junho", 6, 20)]
        [InlineData("Atualizado desde 20/Julho", 7, 20)]
        [InlineData("Atualizado desde 20/Agosto", 8, 20)]
        [InlineData("Atualizado desde 20/Setembro", 9, 20)]
        [InlineData("Atualizado desde 20/Outubro", 10, 20)]
        [InlineData("Atualizado desde 20/Novembro", 11, 20)]
        [InlineData("Atualizado desde 20/Dezembro", 12, 20)]
        public void ParseDateGivenCorrectInput(string dateInput, int expectedMonth, int expectedDay)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();

            DateTimeOffset dateTime = parser.ParseDate(dateInput);
            
            Assert.Equal(expectedMonth, dateTime.Month);
            Assert.Equal(expectedDay, dateTime.Day);
            Assert.Equal(2013, dateTime.Year);
        }
        
        [Theory]
        [InlineData("21/Janeiro")]
        [InlineData("2013-09-23")]
        [InlineData("23-09-2013")]
        [InlineData("2013")]
        [InlineData("Janeiro")]
        [InlineData("21")]
        public void ThrowArgumentExceptionGivenUnsupportedDateInput(string dateInput)
        {
            KinguilaHojeTextualInformationParser parser = GetDefaultParser();
            
            var exception = Assert.Throws<ArgumentException>(() =>  parser.ParseDate(dateInput));
            
            Assert.Equal("date", exception.ParamName);
            Assert.Contains("is invalid", exception.Message);
        }

        public KinguilaHojeTextualInformationParser GetDefaultParser()
        {
            Mock<IDateProvider> dateProviderStub = new Mock<IDateProvider>();
            dateProviderStub
                .Setup(x => x.GetCurrentDate())
                .Returns(new DateTimeOffset(DateTime.Parse("2013-09-23")));
            
            return new KinguilaHojeTextualInformationParser(dateProviderStub.Object);
        }
    }
}