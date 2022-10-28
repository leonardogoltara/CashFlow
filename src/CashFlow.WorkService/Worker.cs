using CashFlow.Common.ExtensionsMethods;
using CashFlow.Domain.Services;

namespace CashFlow.WorkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            List<DateTime> dates = new List<DateTime>()
            {
                new DateTime(2022,10,25),
                new DateTime(2022,10,26),
                new DateTime(2022,10,27),
                new DateTime(2022,10,28)
            };

            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Lê a mensagem da fila
                using (var scope = _serviceScopeFactory.CreateScope())
                {
                    try
                    {
                        var consumerService = scope.ServiceProvider.GetRequiredService<CashFlowConsolidationService>();
                        foreach (var date in dates)
                        {
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
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.GetCompleteMessage());
                    }
                }

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}