using CashFlow.Common.Messaging;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;

namespace CashFlow.Messaging
{
    public class MessageReceiver : IMessageReceiver
    {
        IMessageQueueConfiguration _messageQueueConfiguration;
        public MessageReceiver(IMessageQueueConfiguration messageQueueConfiguration)
        {
            _messageQueueConfiguration = messageQueueConfiguration;
        }

        public async Task<IMessage> Receive()
        {
            IMessage message = null;
            var factory = new ConnectionFactory()
            {
                HostName = _messageQueueConfiguration.QueueUrl,
                UserName = _messageQueueConfiguration.UserName,
                Password = _messageQueueConfiguration.Password
            };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: _messageQueueConfiguration.QueueName,
                                     durable: true,
                                     exclusive: false,
                                     autoDelete: false,
                                     arguments: null);

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var smessage = Encoding.UTF8.GetString(body);
                    message.Body = smessage;

                    Console.WriteLine(" [x] Received {0}", message);
                };
                channel.BasicConsume(queue: _messageQueueConfiguration.QueueName,
                                     autoAck: true,
                                     consumer: consumer);

                return message;
            }
        }
    }
}
