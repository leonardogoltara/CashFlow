using System;

namespace CashFlow.Domain.Models
{
    public class ConsolidateYearModel : ConsolidateBase
    {
        public ConsolidateYearModel(int year, decimal cashInAmout, decimal cashOutAmout) : base(cashOutAmout, cashInAmout)
        {
            Year = year;
        }

        public int Year { get; set; }
    }
}
