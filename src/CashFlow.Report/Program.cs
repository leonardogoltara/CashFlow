using CashFlow.Common.API.Responses;
using CashFlow.Domain.DTOs;
using CashFlow.Domain.Repository;
using CashFlow.Domain.Services;
using CashFlow.Persistence;
using CashFlow.Persistence.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
AppContext.SetSwitch("Npgsql.DisableDateTimeInfinityConversions", true);

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("cashflowdb");

builder.Services.AddDbContext<CashFlowDataContext>(opt =>
{
    if (string.IsNullOrEmpty(connectionString))
        opt.UseInMemoryDatabase("cashflowdb");
    else
        opt.UseNpgsql(connectionString);
}, ServiceLifetime.Singleton);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    opt.EnableAnnotations();
    opt.TagActionsBy(d => new List<string> { d.ActionDescriptor.DisplayName });
});

builder.Services.AddTransient<ICashInRepository, CashInRepository>();
builder.Services.AddTransient<ICashOutRepository, CashOutRepository>();
builder.Services.AddTransient<IConsolidateDayRepository, ConsolidateDayRepository>();
builder.Services.AddTransient<IConsolidateMonthRepository, ConsolidateMonthRepository>();
builder.Services.AddTransient<IConsolidateYearRepository, ConsolidateYearRepository>();
builder.Services.AddTransient<CashFlowConsolidationService>();

var app = builder.Build();

app.AppUseMigrations();
app.UseSwagger();
app.UseSwaggerUI();

app.MapGet("/", () => "Welcome to CashFlow Consolidated.");

app.MapPost("ConsolidatedCash", async (CashFlowConsolidationService service, [FromBody] ConsolidatedDTO dto) =>
{
    var result = await service.GetConsolidated(dto.Date);
    return AppResults.Ok(result);
});

app.Run();
