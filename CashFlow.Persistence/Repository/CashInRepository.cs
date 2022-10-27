using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence.Repository
{
    public class CashInRepository : ICashInRepository
    {
        private CashFlowDataContext _context;

        public CashInRepository(CashFlowDataContext context)
        {
            this._context = context;
        }

        public async Task<CashInModel> Get(long id)
        {
            return await _context.CashIns.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save(CashInModel entity)
        {
            if (entity.Id == 0)
            {
                await _context.CashIns.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                _context.CashIns.Attach(entity);
            }

            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<decimal> SumActiveAmountByDay(DateTime date, bool isActive)
        {
            var amout = await _context.CashIns
                .Where(x => x.Date == date && x.IsActive == isActive)
                .Select(x => x.Amount)
                .SumAsync();

            return amout;
        }
    }
}
