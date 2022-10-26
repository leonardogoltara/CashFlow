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
                cfg.CreateMap<CashIn, CashInResult>();
                cfg.CreateMap<CashOut, CashOutResult>();
            });

            IMapper mapper = config.CreateMapper();

            return mapper;
        }
    }
}
