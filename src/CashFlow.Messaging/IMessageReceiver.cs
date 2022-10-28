using CashFlow.Common.Messaging;

namespace CashFlow.Messaging
{
    public interface IMessageReceiver
    {
        Task<IMessage> Receive();
    }
}