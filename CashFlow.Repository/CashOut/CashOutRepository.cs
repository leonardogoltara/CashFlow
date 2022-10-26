using CashFlow.Domain.Repository;

namespace CashFlow.Repository.CashOut
{
    public class CashOutRepository : ICashOutRepository
    {
        public Task<Domain.Models.CashOutModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(Domain.Models.CashOutModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
