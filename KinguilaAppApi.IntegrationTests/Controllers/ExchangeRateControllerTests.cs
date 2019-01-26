using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using KinguilaAppApi.ViewModels;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace KinguilaAppApi.IntegrationTests.Controllers
{
    public class ExchangeRateControllerTests : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _factory;

        public ExchangeRateControllerTests(WebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetExchangesShouldGetExchangeRatesFromAllSourcesGivenSourceSpecifiedAsAll()
        {
            HttpClient client = _factory.CreateDefaultClient();

            HttpResponseMessage response = await client.GetAsync("/api/v1/exchanges/all");
            
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            
            var exchangeRates = await response.Content.ReadAsAsync<IEnumerable<ExchangeRatesViewModel>>();
            
            Assert.NotNull(exchangeRates);
            Assert.NotEmpty(exchangeRates);
        }
    }
}