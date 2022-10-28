using AutoMapper;
using CashFlow.Domain.DTOs;
using CashFlow.Domain.Models;
using CashFlow.Domain.Results;

namespace CashFlow.Domain
{
    public static class Mapper
    {
        public static IMapper GetMaps()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CashInModel, CashInResult>();
                cfg.CreateMap<CashOutModel, CashOutResult>();
                cfg.CreateMap<ConsolidateDayModel, ConsolidateDayResult>();
                cfg.CreateMap<ConsolidateMonthModel, ConsolidateMonthResult>();
                cfg.CreateMap<ConsolidateYearModel, ConsolidateYearResult>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
