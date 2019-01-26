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
    [Route("api/v1/exchanges")]
    public class ExchangeRateController : Controller
    {
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
        
        [HttpGet("{currency}")]
        public async Task<IActionResult> Last([FromRoute]string currency)
        {
            if (currency.Equals("all"))
            {
                IEnumerable<ExchangeRates> exchangeRates = await _exchangeRateService.GetExchangeRateForAllCurrencies();
                return Ok(_mapper.Map<IEnumerable<ExchangeRatesViewModel>>(exchangeRates));
            }

            return NotFound();
        }
    }
}
