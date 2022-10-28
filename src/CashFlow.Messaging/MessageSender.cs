using CashFlow.Common.ExtensionsMethods;
using CashFlow.Common.Messaging;
using RabbitMQ.Client;
using System.Text;

namespace CashFlow.Messaging
{
    internal class MessageSender : IMessageSender
    {
        IMessageQueueConfiguration _messageQueueConfiguration;
        public MessageSender(IMessageQueueConfiguration messageQueueConfiguration)
        {
            _messageQueueConfiguration = messageQueueConfiguration; 
        }
        public async Task<bool> Send(IMessage message)
        {
			try
			{
                var factory = new ConnectionFactory() { HostName = _messageQueueConfiguration.QueueUrl };
                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: _messageQueueConfiguration.QueueName,
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var body = Encoding.UTF8.GetBytes(message.Body);

                    channel.BasicPublish(exchange: "",
                                         routingKey: message.RoutingKey,
                                         basicProperties: null,
                                         body: body);

                    Console.WriteLine(" [x] Sent {0}", message);
                }

                return true;
            }
			catch (Exception ex)
			{
				Console.WriteLine(ex.GetCompleteMessage());
				return false;
			}
        }
    }
}
