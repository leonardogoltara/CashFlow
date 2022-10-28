namespace CashFlow.Common.Messaging
{
    public interface IMessageQueueConfiguration
    {
        public string QueueUrl { get; set; }
        public string QueueName { get; set; }
    }
}
