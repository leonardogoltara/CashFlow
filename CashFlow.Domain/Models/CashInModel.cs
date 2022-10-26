namespace CashFlow.Domain.Models
{
    public class CashInModel : ModelBase
    {
        public CashInModel() : base() { }
        public CashInModel(decimal amount, DateTime dateTime) : this()
        {
            Amount = amount;
            DateTime = dateTime;
        }

        public CashInModel(int id, decimal amount, DateTime dateTime) : this(amount, dateTime)
        {
            SetId(id);
        }

        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
