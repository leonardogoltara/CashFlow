namespace CashFlow.Common.Messaging
{
    public interface IMessageSender
    {
        public Task<bool> Send(IMessage message);
    }
}
