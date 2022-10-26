namespace CashFlow.Domain.Models
{
    public class CashOutModel : ModelBase
    {
        public CashOutModel() : base() { }
        public CashOutModel(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
