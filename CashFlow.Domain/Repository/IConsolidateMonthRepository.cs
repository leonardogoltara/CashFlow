using CashFlow.Domain.Models;
using CashFlow.Domain.Results;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateMonthRepository
    {
        public Task<bool> Save(ConsolidateMonthModel entity);
        public Task<ConsolidateMonthModel> Get(DateTime dateTime);
        public Task<ConsolidateMonthResult> SumAmountByYear(int year);
    }
}
