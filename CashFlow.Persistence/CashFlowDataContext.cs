using CashFlow.Domain.Models;
using CashFlow.Persistence.Mappings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Persistence
{
    public class CashFlowDataContext : DbContext
    {
        public CashFlowDataContext() : base()
        {
        }

        public CashFlowDataContext(DbContextOptions<CashFlowDataContext> options) : base(options)
        {
        }

        public DbSet<CashInModel> CashIns { get; set; }
        public DbSet<CashOutModel> CashOuts { get; set; }
        public DbSet<ConsolidateDayModel> ConsolidateDays { get; set; }
        public DbSet<ConsolidateMonthModel> ConsolidateMonths { get; set; }
        public DbSet<ConsolidateYearModel> ConsolidateYears { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("User ID=postgres;Password=Postgres2018!;Host=localhost;Port=5432;Database=postgres;Pooling=false");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("cashflow");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(CashInMap).GetTypeInfo().Assembly);
        }
    }
}
