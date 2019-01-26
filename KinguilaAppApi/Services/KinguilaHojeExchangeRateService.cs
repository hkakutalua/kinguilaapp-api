using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using KinguilaAppApi.ApiMapping;
using KinguilaAppApi.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace KinguilaAppApi.Services
{
    public class KinguilaHojeExchangeRateService : IExchangeRateService
    {
        private readonly IPageTextualInformationParser _pageTextualInformationParser;
        private readonly KinguilaHojeSourceMapper _kinguilaHojeSourceMapper;

        public KinguilaHojeExchangeRateService(IPageTextualInformationParser pageTextualInformationParser,
            KinguilaHojeSourceMapper kinguilaHojeSourceMapper)
        {
            if (pageTextualInformationParser == null)
                throw new ArgumentNullException(nameof(pageTextualInformationParser));
            if (kinguilaHojeSourceMapper == null)
                throw new ArgumentNullException(nameof(kinguilaHojeSourceMapper));

            _pageTextualInformationParser = pageTextualInformationParser;
            _kinguilaHojeSourceMapper = kinguilaHojeSourceMapper;
        }
        
        public async Task<IEnumerable<ExchangeRates>> GetExchangeRateFromAllSources()
        {
            Uri kinguilaHojeUri = new Uri("http://www.kinguilahoje.com/");
            
            ScrapingBrowser browser = new ScrapingBrowser();
            WebPage kinguilaHomePage = await browser.NavigateToPageAsync(kinguilaHojeUri);

            return GetExchangeRatesFromSources(ExchangeRateSource.GetAll(), kinguilaHomePage.Html);
        }

        private IEnumerable<ExchangeRates> GetExchangeRatesFromSources(IEnumerable<ExchangeRateSource> exchangeRateSources, HtmlNode rootNode)
        {
            const string ExchangeRateDiv = "div.col-xs-12.gray-round-1px-border";
            const string ExchangeRateSourceHeading = "h3#homeHeading";
            const string ExchangeRateUpdateDateParagraph = "p.no-margin.full-width.black-text";
            
            IEnumerable<HtmlNode> exchangeRateHtmlNodes = rootNode.CssSelect(ExchangeRateDiv);
            
            foreach (HtmlNode exchangeRateNode in exchangeRateHtmlNodes)
            {
                string exchangeRateSourceText = exchangeRateNode.CssSelect(ExchangeRateSourceHeading).First().InnerText;
                string updatedAtText = exchangeRateNode.CssSelect(ExchangeRateUpdateDateParagraph).First().InnerText;
                
                if (!_kinguilaHojeSourceMapper.CanMap(exchangeRateSourceText))
                    continue;

                ExchangeRateSource exchangeRateSource = _kinguilaHojeSourceMapper.Map(exchangeRateSourceText);
                if (!exchangeRateSources.Contains(exchangeRateSource))
                    continue;

                DateTimeOffset updatedAt = _pageTextualInformationParser.ParseDate(updatedAtText);
                IEnumerable<CurrencyRate> currencyRates = GetCurrencyRates(exchangeRateNode);
                
                yield return new ExchangeRates(exchangeRateSource, updatedAt, currencyRates);
            }
        }

        private IEnumerable<CurrencyRate> GetCurrencyRates(HtmlNode exchangeRateNode)
        {
            const string CurrencyRateSpan = "span.col-xs-12.quotation";
            
            IEnumerable<HtmlNode> currencyRateNodes = exchangeRateNode.CssSelect(CurrencyRateSpan);
            
            foreach (HtmlNode currencyRateNode in currencyRateNodes)
            {
                string currencyRate = currencyRateNode.InnerText;
                yield return _pageTextualInformationParser.ParseCurrencyRate(currencyRate);
            }
        }
    }
}