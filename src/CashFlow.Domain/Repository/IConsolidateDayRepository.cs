using CashFlow.Domain.Models;
using CashFlow.Domain.Results;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateDayRepository
    {
        public Task<bool> Save(ConsolidateDayModel entity);
        public Task<ConsolidateDayModel> Get(DateTime date);
        public Task<ConsolidateDayResult> SumAmountByMonth(DateTime date);
    }
}
