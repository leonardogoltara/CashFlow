namespace CashFlow.Domain.Models
{
    public abstract class ConsolidateBase
    {
        public ConsolidateBase()
        {
            UpdatedDate = DateTime.Now;
        }

        public ConsolidateBase(decimal cashInAmout, decimal cashOutAmout) : this()
        {
            CashInAmout = cashInAmout;
            CashOutAmout = cashOutAmout;
        }

        public decimal CashInAmout { get; private set; }
        public decimal CashOutAmout { get; private set; }
        public int Id { get; private set; }
        public DateTime UpdatedDate { get; private set; }

        protected void SetId(int id)
        {
            Id = id;
        }

        public void Update(decimal cashInAmout, decimal cashOutAmout)
        {
            UpdatedDate = DateTime.Now;
            CashInAmout = cashInAmout;
            CashOutAmout = cashOutAmout;
        }
    }
}
