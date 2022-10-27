using CashFlow.Domain.Models;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Results;
using Microsoft.EntityFrameworkCore;

namespace CashFlow.Persistence.Repository
{
    public class ConsolidateMonthRepository : IConsolidateMonthRepository
    {
        private CashFlowDataContext _context;

        public ConsolidateMonthRepository(CashFlowDataContext context)
        {
            this._context = context;
        }

        public async Task<ConsolidateMonthModel> Get(DateTime date)
        {
            return await _context.ConsolidateMonths.FirstOrDefaultAsync(x => x.Month == date);
        }

        public async Task<bool> Save(ConsolidateMonthModel entity)
        {
            if (entity.Id == 0)
            {
                await _context.ConsolidateMonths.AddAsync(entity).ConfigureAwait(false);
            }
            else
            {
                _context.ConsolidateMonths.Attach(entity);
            }

            var result = await _context.SaveChangesAsync();
            return (result > 0);
        }

        public async Task<ConsolidateMonthResult> SumAmountByYear(int year)
        {
            var result = await _context.ConsolidateMonths
                 .Where(x => x.Month.Year == year)
                 .GroupBy(x => x.Month)
                 .Select(x => new ConsolidateMonthResult()
                 {
                     CashInAmout = x.Select(c => c.CashInAmout).Sum(),
                     CashOutAmout = x.Select(c => c.CashOutAmout).Sum()
                 }).FirstOrDefaultAsync();

            return result;
        }
    }
}
