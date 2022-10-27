using CashFlow.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Repository
{
    public interface IConsolidateYearRepository
    {
        public Task<bool> Save(ConsolidateYearModel entity);
        public Task<ConsolidateYearModel> Get(int year);
    }
}
