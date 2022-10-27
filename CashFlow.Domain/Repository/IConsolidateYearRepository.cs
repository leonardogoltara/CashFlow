using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateYearRepository
    {
        public Task<bool> Save(ConsolidateYearModel entity);
        public Task<ConsolidateYearModel> Get(int year);
    }
}
