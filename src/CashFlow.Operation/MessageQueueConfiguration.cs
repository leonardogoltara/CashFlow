using CashFlow.Common.Messaging;

namespace CashFlow.Operation
{
    public class MessageQueueConfiguration : IMessageQueueConfiguration
    {
        public string QueueUrl => "localhost";
        
        public string QueueName => "cashflow";

        public string UserName => "test-user";

        public string Password => "test-user";
    }
}
