using System;
using KinguilaAppApi.Models;

namespace KinguilaAppApi.Services
{
    public interface IPageTextualInformationParser
    {
        /// <summary>
        /// Parses the currency rate string to an <see cref="CurrencyRate"/> string type object
        /// </summary>
        /// <param name="currencyRate">The currency rate string to parse</param>
        /// <returns>An <see cref="CurrencyRate"/> object</returns>
        CurrencyRate ParseCurrencyRate(string currencyRate);

        /// <summary>
        /// Parses the date to an <see cref="DateTimeOffset" object/>
        /// </summary>
        /// <param name="date">The date to parse</param>
        /// <returns>A <see cref="DateTimeOffset"/> object</returns>
        DateTimeOffset ParseDate(string date);
    }
}