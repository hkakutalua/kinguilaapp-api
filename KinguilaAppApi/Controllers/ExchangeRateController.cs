using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using KinguilaAppApi.Models;
using KinguilaAppApi.Services;
using KinguilaAppApi.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace KinguilaAppApi.Controllers
{
    [Route("api/v1/exchange")]
    public class ExchangeRateController : Controller
    {
        private const string AllCurrenciesCode = "all";
        private const string UnitedStatesDollarCode = "usd";
        private const string EuroCode = "eur";
        
        private readonly IExchangeRateService _exchangeRateService;
        private readonly IMapper _mapper;

        public ExchangeRateController(IExchangeRateService exchangeRateService, IMapper mapper)
        {
            if (exchangeRateService == null)
                throw new ArgumentNullException(nameof(exchangeRateService));
            if (mapper == null)
                throw new ArgumentNullException(nameof(mapper));
            
            _exchangeRateService = exchangeRateService;
            _mapper = mapper;
        }
        
        [HttpGet("{currency}/[action]")]
        public async Task<IActionResult> Last([FromRoute]string currency)
        {
            IEnumerable<ExchangeRates> exchangeRates = await _exchangeRateService.GetExchangeRateForAllCurrencies();
            return Ok(_mapper.Map<IEnumerable<ExchangeRatesViewModel>>(exchangeRates));
        }
    }
}
