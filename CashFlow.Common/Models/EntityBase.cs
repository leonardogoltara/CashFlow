namespace CashFlow.Common.Models
{
    public abstract class EntityBase
    {
        public int Id { get; }
        public DateTime UpdatedDate { get; protected set; }
        public DateTime? CancelationDate { get; protected set; }
        public bool IsActive { get; protected set; }

        public EntityBase()
        {
            IsActive = true;
            UpdatedDate = DateTime.Now;
            CancelationDate = null;
        }

        protected void Cancel()
        {
            CancelationDate = DateTime.Now;
            UpdatedDate = DateTime.Now;
            IsActive = false;
        }
    }
}
