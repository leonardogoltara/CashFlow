namespace CashFlow.Common.DTOs
{
    public abstract class BaseResult
    {
        public int Id { get; set; }
        public DateTime UpdatedDate { get; set; }
        public DateTime? CancelationDate { get; set; }
        public bool IsActive { get; set; }

        public string? ErrorMessage { get; set; }
        public bool HasError { get { return !string.IsNullOrWhiteSpace(ErrorMessage); } }
        public void SetErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
