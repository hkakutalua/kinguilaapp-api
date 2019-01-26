using AutoMapper;
using KinguilaAppApi.Models;
using KinguilaAppApi.ViewModels;

namespace KinguilaAppApi.ApiMapping
{
    public class ExchangeRatesMappingProfile : Profile
    {
        public ExchangeRatesMappingProfile()
        {
            CreateMap<ExchangeRates, ExchangeRatesViewModel>()
                .ForMember(destination => destination.Source, config => config.MapFrom(source => source.Source.ToString().ToLowerInvariant()))
                .ForMember(destination => destination.UpdatedAt, config => config.MapFrom(source => source.UpdatedAt))
                .ForMember(destination => destination.CurrencyRates, config => config.MapFrom(source => source.CurrencyRates));
        }
    }
}