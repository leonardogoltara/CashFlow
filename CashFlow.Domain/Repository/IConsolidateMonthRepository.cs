using CashFlow.Domain.Models;
using CashFlow.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateMonthRepository
    {
        public Task<bool> Save(ConsolidateMonthModel entity);
        public Task<ConsolidateMonthModel> Get(DateTime dateTime);
        public Task<ConsolidateMonthResult> SumAmountByYear(int year);
    }
}
