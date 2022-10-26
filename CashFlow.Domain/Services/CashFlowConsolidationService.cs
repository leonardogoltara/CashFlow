using CashFlow.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Domain.Services
{
    public class CashFlowConsolidationService
    {
        public async Task<bool> ConsolidateDay() { return await Task.FromResult(true); }
        public async Task<bool> ConsolidateMonth() { return await Task.FromResult(true); }
        public async Task<bool> ConsolidateYear() { return await Task.FromResult(true); }
    }
}
