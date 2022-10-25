namespace CashFlow.Common.Models
{
    public class CashOut : EntityBase
    {
        public CashOut() : base() { }
        public CashOut(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }
        public CashOut(int cashInReference, decimal amount, DateTime dateTime) : this(amount, dateTime)
        {
            CashInReference = cashInReference;
        }

        public decimal Amount { get; }
        public DateTime DateTime { get; }
        public int? CashInReference { get; }
    }
}
