using CashFlow.Domain.Repository;

namespace CashFlow.Repository.CashOut
{
    public class CashOutRepository : ICashOutRepository
    {
        public Task<Domain.Models.CashOut> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(Domain.Models.CashOut entity)
        {
            throw new NotImplementedException();
        }
    }
}
