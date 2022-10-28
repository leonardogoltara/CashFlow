namespace CashFlow.Common.Messaging
{
    public interface IMessage
    {
        public object Body { get; set; }
        public string RoutingKey { get; set; }
    }
}
