using AutoMapper;
using CashFlow.Domain.DTOs;
using CashFlow.Domain.Models;

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
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
