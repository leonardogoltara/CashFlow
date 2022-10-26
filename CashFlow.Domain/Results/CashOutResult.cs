namespace CashFlow.Domain.DTOs
{
    public class CashOutResult : BaseResult
    {
        public decimal Amount { get; private set;  }
        public DateTime DateTime { get; private set;  }
    }
}
