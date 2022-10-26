namespace CashFlow.WorkService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

                // Lê a mensagem da fila

                // Se houver
                // ConsolidationService.ConsolidateDay
                // ConsolidationService.ConsolidateMonth
                // ConsolidationService.ConsolidateYear

                await Task.Delay(5000, stoppingToken);
            }
        }
    }
}