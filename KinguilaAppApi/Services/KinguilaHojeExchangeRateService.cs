using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using KinguilaAppApi.Models;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace KinguilaAppApi.Services
{
    public class KinguilaHojeExchangeRateService : IExchangeRateService
    {
        private const string KinguilaHojeAddress = "http://www.kinguilahoje.com/";
        private const string ExchangeRateDiv = "div.col-xs-12.gray-round-1px-border";
        
        public async Task<IEnumerable<ExchangeRates>> GetExchangeRateForAllCurrencies()
        {
            ScrapingBrowser browser = new ScrapingBrowser();

            WebPage kinguilaHomePage = await browser.NavigateToPageAsync(new Uri(KinguilaHojeAddress));
            HtmlNode htmlNode = kinguilaHomePage.Html;

            return GetExchangeRatesFromSources(ExchangeRates.GetExchangeRatesSources(), htmlNode);
        }

        private IEnumerable<ExchangeRates> GetExchangeRatesFromSources(IEnumerable<string> sources, HtmlNode rootNode)
        {
            IEnumerable<HtmlNode> exchangeRateHtmlNodes = rootNode.CssSelect(ExchangeRateDiv);
            
            foreach (HtmlNode exchangeRateNode in exchangeRateHtmlNodes)
            {
                String source = exchangeRateNode.CssSelect("h3#homeHeading").First().InnerText;
                
                if (!sources.Contains(source.ToLowerInvariant()))
                    continue;
                
                String updatedAt = exchangeRateNode.CssSelect("p.no-margin.full-width.black-text").First().InnerText;
                IEnumerable<CurrencyRate> currencyRates = GetCurrencyRates(exchangeRateNode);
                
                yield return ExchangeRates.Parse(source, updatedAt, currencyRates, DateProvider.Default());
            }
        }

        private IEnumerable<CurrencyRate> GetCurrencyRates(HtmlNode exchangeRateNode)
        {
            IEnumerable<HtmlNode> currencyRateNodes = exchangeRateNode.CssSelect("span.col-xs-12.quotation");
            foreach (HtmlNode currencyRateNode in currencyRateNodes)
            {
                string currencyRate = currencyRateNode.InnerText;
                yield return CurrencyRate.Parse(currencyRate);
            }
        }
    }
}