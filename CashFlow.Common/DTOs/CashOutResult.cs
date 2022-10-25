namespace CashFlow.Common.DTOs
{
    public class CashOutResult : BaseResult
    {
        public decimal Amount { get; }
        public DateTime DateTime { get; }
        public int? CashInReference { get; }
    }
}
