namespace CashFlow.Domain.Models
{
    public abstract class EntityBase
    {
        public int Id { get; }
        public DateTime UpdatedDate { get; private set; }
        public DateTime? CancelationDate { get; private set; }
        public bool IsActive { get; private set; }

        public EntityBase()
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
    }
}
