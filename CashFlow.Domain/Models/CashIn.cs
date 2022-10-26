namespace CashFlow.Domain.Models
{
    public class CashIn : EntityBase
    {
        public CashIn() : base() { }
        public CashIn(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
