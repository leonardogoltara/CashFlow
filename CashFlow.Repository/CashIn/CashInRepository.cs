using CashFlow.Domain.Repository;

namespace CashFlow.Repository.CashIn
{
    public class CashInRepository : ICashInRepository
    {
        public Task<Domain.Models.CashIn> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(Domain.Models.CashIn entity)
        {
            throw new NotImplementedException();
        }
    }
}
