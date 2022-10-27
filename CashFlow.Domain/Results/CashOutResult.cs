namespace CashFlow.Domain.DTOs
{
    public class CashOutResult : BaseResult
    {
        public decimal Amount { get; private set;  }
        public DateTime Date { get; private set;  }
    }
}
