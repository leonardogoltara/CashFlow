namespace CashFlow.Domain.DTOs
{
    public class CashInResult : BaseResult
    {
        public decimal Amount { get; private set; }
        public DateTime DateTime { get; private set; }
    }
}
