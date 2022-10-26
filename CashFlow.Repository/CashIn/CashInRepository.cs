using CashFlow.Domain.Repository;

namespace CashFlow.Repository.CashIn
{
    public class CashInRepository : ICashInRepository
    {
        public Task<Domain.Models.CashInModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Save(Domain.Models.CashInModel entity)
        {
            throw new NotImplementedException();
        }
    }
}
