using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence.Repository
{
    public class ConsolidateYearRepository : IConsolidateYearRepository
    {
        private CashFlowDataContext _context;

        public ConsolidateYearRepository(CashFlowDataContext context)
        {
            this._context = context;
        }

        public async Task<ConsolidateYearModel> Get(int year)
        {
            return await _context.ConsolidateYears.FirstOrDefaultAsync(x => x.Year == year);
        }

        public async Task<bool> Save(ConsolidateYearModel entity)
        {
            if (entity.Id == 0)
            {
                await _context.ConsolidateYears.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                _context.ConsolidateYears.Attach(entity);
            }

            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }
    }
}
