using CashFlow.Domain.Models;

namespace CashFlow.Domain.Repository
{
    public interface IRepository<T> where T : ModelBase
    {
        public Task<bool> Save(T entity);
        public Task<T> Get(int id);
        public Task<decimal> SumActiveAmountByDay(DateTime dateTime, bool isActive);
    }
}
