namespace CashFlow.Domain.Models
{
    public class ConsolidateMonthModel : ConsolidateBase
    {
        public ConsolidateMonthModel(DateTime month, decimal cashInAmout, decimal cashOutAmout) : base(cashOutAmout, cashInAmout)
        {
            Month = new DateTime(month.Year, month.Month, 1);
        }

        public DateTime Month { get; set; }
    }
}
