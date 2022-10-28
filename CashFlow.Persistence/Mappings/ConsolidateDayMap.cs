using CashFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Persistence.Mappings
{
    public class ConsolidateDayMap : IEntityTypeConfiguration<ConsolidateDayModel>
    {
        public void Configure(EntityTypeBuilder<ConsolidateDayModel> builder)
        {
            builder.ToTable("ConsolidateDay");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnType("bigint")
                .IsRequired();

            builder.HasIndex(c => c.Day).IsUnique();

            builder.Property(c => c.Day)
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