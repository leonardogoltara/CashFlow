namespace CashFlow.Domain.Models
{
    public class CashOutModel : ModelBase
    {
        public CashOutModel() : base() { }
        public CashOutModel(decimal amount, DateTime date) : this()
        {
            Amount = amount;
            Date = date;
        }

        public CashOutModel(long id, decimal amount, DateTime date) : this(amount, date)
        {
            SetId(id);
        }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
    }
}
