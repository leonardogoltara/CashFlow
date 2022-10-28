namespace CashFlow.Domain.Models
{
    public class ConsolidateDayModel : ConsolidateBase
    {
        public ConsolidateDayModel(DateTime day, decimal cashInAmout, decimal cashOutAmout) : base(cashOutAmout, cashInAmout)
        {
            Day = day.Date;
        }

        public DateTime Day { get; private set; }

    }
}
