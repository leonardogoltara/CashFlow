using CashFlow.Domain.Models;
using CashFlow.Domain.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateDayRepository
    {
        public Task<bool> Save(ConsolidateDayModel entity);
        public Task<ConsolidateDayModel> Get(DateTime dateTime);
        public Task<ConsolidateDayResult> SumAmountByMonth(DateTime dateTime);
    }
}
