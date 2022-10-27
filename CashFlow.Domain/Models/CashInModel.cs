namespace CashFlow.Domain.Models
{
    public class CashInModel : ModelBase
    {
        public CashInModel() : base() { }
        public CashInModel(decimal amount, DateTime date) : this()
        {
            Amount = amount;
            Date = date;
        }

        public CashInModel(long id, decimal amount, DateTime date) : this(amount, date)
        {
            SetId(id);
        }

        public decimal Amount { get; private set; }
        public DateTime Date { get; private set; }
    }
}
