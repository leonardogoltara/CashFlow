namespace CashFlow.Common.DTOs
{
    public class CashInResult : BaseResult
    {
        public decimal Amount { get; set; }
        public DateTime DateTime { get; set; }
        public int? CashOutReference { get; set; }
    }
}
