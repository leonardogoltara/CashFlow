namespace CashFlow.Common.Messaging
{
    public interface IMessage
    {
        public string Body { get; set; }
        public string RoutingKey { get; set; }
    }
}
