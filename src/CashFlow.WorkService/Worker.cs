using CashFlow.Common.ExtensionsMethods;
using CashFlow.Common.Messaging;
using CashFlow.Domain.Services;

namespace CashFlow.WorkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly IMessageReceiver _messageReceiver;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory, IMessageReceiver messageReceiver)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
            _messageReceiver = messageReceiver;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                var message = await _messageReceiver.Receive();
                if (message != null)
                {
                    // Deserializa o message.Body;

                    // Lê a mensagem da fila
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        try
                        {
                            var consumerService = scope.ServiceProvider.GetRequiredService<CashFlowConsolidationService>();

                            var result = await consumerService.ConsolidateDay(date);
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
                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}