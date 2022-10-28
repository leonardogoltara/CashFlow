using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using CashFlow.Common.ExtensionsMethods;
using CashFlow.Common.JsonHelper;
using CashFlow.Domain.Models;
using CashFlow.Domain.Services;
using CashFlow.Common.Messaging;
using CashFlow.Domain.DTOs;

namespace CashFlow.WorkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly int _intervaloMensagemWorkerAtivo;
        private readonly IMessageQueueConfiguration _messageQueueConfiguration;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IMessageQueueConfiguration messageQueueConfiguration, IServiceScopeFactory serviceScopeFactory)
        {
            logger.LogInformation($"Queue = {messageQueueConfiguration.QueueName}");

            _logger = logger;
            _intervaloMensagemWorkerAtivo = 60000;
            _messageQueueConfiguration = messageQueueConfiguration;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation(
                "Aguardando mensagens...");

            var factory = new ConnectionFactory()
            {
                HostName = _messageQueueConfiguration.QueueUrl,
                UserName = _messageQueueConfiguration.UserName,
                Password = _messageQueueConfiguration.Password
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(queue: _messageQueueConfiguration.QueueName,
                                durable: true,
                                exclusive: false,
                                autoDelete: false,
                                arguments: null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += Consumer_Received;
            channel.BasicConsume(queue: _messageQueueConfiguration.QueueName,
                autoAck: true,
                consumer: consumer);

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation(
                    $"Worker ativo em: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
                await Task.Delay(_intervaloMensagemWorkerAtivo, stoppingToken);
            }
        }

        private void Consumer_Received(
            object sender, BasicDeliverEventArgs e)
        {
            var body = e.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            _logger.LogInformation($"[Nova mensagem | {DateTime.Now:yyyy-MM-dd HH:mm:ss}] " + message);

            DateTime date;
            var cashin = JsonUtils.Deserialize<CashInDTO>(message);
            date = cashin?.Date ?? DateTime.MinValue;

            if (date != DateTime.MinValue)
            {
                // Lê a mensagem da fila
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var consumerService = scope.ServiceProvider.GetRequiredService<CashFlowConsolidationService>();

                        var result = consumerService.ConsolidateDay(date).GetAwaiter().GetResult();
                        if (result)
                        {
                            Console.WriteLine($"Consolidated day: {date.ToShortDateString()}");
                        }
                        else
                        {
                            Console.WriteLine($"Fail tring to consolidate day: {date.ToShortDateString()}");
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.GetCompleteMessage());
                    }
                }
            }
        }
    }
}
