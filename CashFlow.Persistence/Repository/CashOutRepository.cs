using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence.Repository
{
    public class CashOutRepository : ICashOutRepository
    {
        private CashFlowDataContext _context;

        public CashOutRepository(CashFlowDataContext context)
        {
            this._context = context;
        }

        public async Task<CashOutModel> Get(long id)
        {
            return await _context.CashOuts.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> Save(CashOutModel entity)
        {
            if (entity.Id == 0)
            {
                await _context.CashOuts.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                _context.CashOuts.Attach(entity);
            }

            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public Task<decimal> SumActiveAmountByDay(DateTime date, bool isActive)
        {
            var amout = _context.CashOuts
                .Where(x => x.Date == date && x.IsActive == isActive)
                .Select(x => x.Amount)
                .SumAsync();

            return amout;
        }
    }
}
