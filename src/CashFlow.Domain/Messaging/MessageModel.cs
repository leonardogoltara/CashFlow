using CashFlow.Common.Messaging;

namespace CashFlow.Domain.Messaging
{
    public class MessageModel : IMessage
    {
        public string Body { get; set; }
        public string RoutingKey { get; set; }
    }
}
