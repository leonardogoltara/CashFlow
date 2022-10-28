using CashFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Persistence.Mappings
{
    public class ConsolidateMonthMap : IEntityTypeConfiguration<ConsolidateMonthModel>
    {
        public void Configure(EntityTypeBuilder<ConsolidateMonthModel> builder)
        {
            builder.ToTable("ConsolidateMonth");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnType("bigint")
                .IsRequired();

            builder.HasIndex(c => c.Month).IsUnique();

            builder.Property(c => c.Month)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(c => c.CashInAmout).IsRequired();
            builder.Property(c => c.CashOutAmout).IsRequired();

            builder.Property(c => c.UpdatedDate)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}