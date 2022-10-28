using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CashFlow.Persistence
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AppAddDatabase(this IServiceCollection services, IConfiguration configuration, string connectionStringKey,
            ServiceLifetime lifeTime = ServiceLifetime.Scoped)
        {
            services.AddDbContext<CashFlowDataContext>(options =>
            {
                options.UseNpgsql(configuration.GetConnectionString(connectionStringKey));
                options.EnableSensitiveDataLogging();
            }, lifeTime);

            return services;
        }

        public static IApplicationBuilder AppUseMigrations(this IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<CashFlowDataContext>();

                if (context.Database.GetPendingMigrations().Any())
                {
                    context.Database.Migrate();
                }
            }
            return app;
        }
    }
}