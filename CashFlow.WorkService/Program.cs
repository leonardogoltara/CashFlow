using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using CashFlow.Persistence;
using CashFlow.Persistence.Repository;
using CashFlow.WorkService;
using Microsoft.EntityFrameworkCore;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();

        services.AddTransient<ICashInRepository, CashInRepository>();
        services.AddTransient<ICashOutRepository, CashOutRepository>();
        services.AddTransient<IConsolidateDayRepository, ConsolidateDayRepository>();
        services.AddTransient<IConsolidateMonthRepository, ConsolidateMonthRepository>();
        services.AddTransient<IConsolidateYearRepository, ConsolidateYearRepository>();

        services.AddTransient<CashFlowConsolidationService>();
        
        var connectionString = hostContext.Configuration.GetConnectionString("cashflowdb");
        services.AddDbContext<CashFlowDataContext>(opt =>
        {
            if (string.IsNullOrEmpty(connectionString))
                opt.UseInMemoryDatabase("cashflowdb");
            else
                opt.UseNpgsql(connectionString);
        }, ServiceLifetime.Singleton);
    })
    .Build();

await host.RunAsync();
