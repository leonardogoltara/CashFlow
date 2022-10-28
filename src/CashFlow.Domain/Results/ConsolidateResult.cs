namespace CashFlow.Domain.Results
{
    public class ConsolidateResult
    {
        public ConsolidateDayResult ConsolidateDayResult { get; set; }
        public ConsolidateMonthResult ConsolidateMonthResult { get; set; }
        public ConsolidateYearResult ConsolidateYearResult { get; set; }

        public string? ErrorMessage { get; private set; }
        public bool HasError { get { return !string.IsNullOrWhiteSpace(ErrorMessage); } }
        public void SetErrorMessage(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}
