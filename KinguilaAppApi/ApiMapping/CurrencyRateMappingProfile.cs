using AutoMapper;
using KinguilaAppApi.Models;
using KinguilaAppApi.ViewModels;

namespace KinguilaAppApi.ApiMapping
{
    public class CurrencyRateMappingProfile : Profile
    {
        public CurrencyRateMappingProfile()
        {
            CreateMap<CurrencyRate, CurrencyRateViewModel>()
                .ForMember(destination => destination.Currency, cfg => cfg.MapFrom(source => source.Currency.ToString()))
                .ForMember(destination => destination.Amount, cfg => cfg.MapFrom(source => source.Amount));
        }
    }
}