using System.Collections.Generic;
using System.Threading.Tasks;
using KinguilaAppApi.Models;

namespace KinguilaAppApi.Services
{
    public interface IExchangeRateService
    {
        Task<IEnumerable<ExchangeRates>> GetExchangeRateForAllCurrencies();
    }
}