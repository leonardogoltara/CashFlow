namespace CashFlow.Domain.Models
{
    public abstract class ModelBase
    {
        public int Id { get; private set; }
        public DateTime UpdatedDate { get; private set; }
        public DateTime? CancelationDate { get; private set; }
        public bool IsActive { get; private set; }

        public ModelBase()
        {
            IsActive = true;
            UpdatedDate = DateTime.Now;
            CancelationDate = null;
        }

        public void Cancel()
        {
            CancelationDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = false;
        }

        protected void SetId(int id)
        {
            Id = id;
        }
    }
}
