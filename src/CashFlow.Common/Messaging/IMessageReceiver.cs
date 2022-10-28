namespace CashFlow.Common.Messaging
{
    public interface IMessageReceiver
    {
        Task<IMessage> Receive();
    }
}