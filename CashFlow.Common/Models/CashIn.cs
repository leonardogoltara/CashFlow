namespace CashFlow.Common.Models
{
    public class CashIn : EntityBase
    {
        public CashIn() : base() { }
        public CashIn(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }
        public CashIn(int cashOutReference, decimal amount, DateTime dateTime) : this(amount, dateTime)
        {
            CashOutReference = cashOutReference;
        }

        public decimal Amount { get; }
        public DateTime DateTime { get; }
        public int? CashOutReference { get; }
    }
}
