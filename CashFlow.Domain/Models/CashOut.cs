namespace CashFlow.Domain.Models
{
    public class CashOut : EntityBase
    {
        public CashOut() : base() { }
        public CashOut(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
