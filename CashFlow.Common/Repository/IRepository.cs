using CashFlow.Common.Models;

namespace CashFlow.Common.Repository
{
    public interface IRepository<T> where T : EntityBase
    {
        public bool Save(T entity);
        public T Get(int id);
    }
}
