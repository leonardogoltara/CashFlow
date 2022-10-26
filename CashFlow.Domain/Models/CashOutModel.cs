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

        public CashOutModel(int id, decimal amount, DateTime dateTime) : this(amount, dateTime)
        {
            SetId(id);
        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
