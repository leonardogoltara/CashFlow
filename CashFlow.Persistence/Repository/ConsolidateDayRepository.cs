using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence.Repository
{
    public class ConsolidateDayRepository : IConsolidateDayRepository
    {
        private CashFlowDataContext _context;

        public ConsolidateDayRepository(CashFlowDataContext context)
        {
            this._context = context;
        }

        public async Task<ConsolidateDayModel> Get(DateTime date)
        {
            return await _context.ConsolidateDays.FirstOrDefaultAsync(x => x.Day == date);
        }

        public async Task<bool> Save(ConsolidateDayModel entity)
        {
            if (entity.Id == 0)
            {
                await _context.ConsolidateDays.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                _context.ConsolidateDays.Attach(entity);
            }

            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<ConsolidateDayResult> SumAmountByMonth(DateTime date)
        {
            var result = await _context.ConsolidateDays
                 .Where(x => x.Day == date)
                 .GroupBy(x => x.Day)
                 .Select(x => new ConsolidateDayResult()
                 {
                     CashInAmout = x.Select(c => c.CashInAmout).Sum(),
                     CashOutAmout = x.Select(c => c.CashOutAmout).Sum()
                 }).FirstOrDefaultAsync();

            return result;
        }
    }
}
