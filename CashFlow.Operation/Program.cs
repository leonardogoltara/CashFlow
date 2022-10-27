using CashFlow.Persistence;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var connectionString = builder.Configuration.GetValue<string>("ConnectionString");

builder.Services.AddDbContext<CashFlowDataContext>(opt =>
{
    if (string.IsNullOrEmpty(connectionString))
        opt.UseInMemoryDatabase("cashflowdb");
    else
        opt.UseNpgsql(connectionString);
}, ServiceLifetime.Singleton);


app.MapGet("/", () => "Hello World!");

app.Run();
