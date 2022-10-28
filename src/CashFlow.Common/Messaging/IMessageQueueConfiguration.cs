namespace CashFlow.Common.Messaging
{
    public interface IMessageQueueConfiguration
    {
        public string QueueUrl { get; }
        public string QueueName { get; }
        public string UserName { get; }
        public string Password { get; }
    }
}
