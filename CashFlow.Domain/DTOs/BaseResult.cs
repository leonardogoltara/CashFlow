namespace CashFlow.Domain.DTOs
{
    public abstract class BaseResult
    {
        public int Id { get; set; }
        public DateTime UpdatedDate { get; private set; }
        public DateTime? CancelationDate { get; private set; }
        public bool IsActive { get; private set; }

        public string? ErrorMessage { get; private set; }
        public bool HasError { get { return !string.IsNullOrWhiteSpace(ErrorMessage); } }
        public void SetErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
