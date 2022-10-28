using CashFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CashFlow.Persistence.Mappings
{
    public class CashOutMap : IEntityTypeConfiguration<CashOutModel>
    {
        public void Configure(EntityTypeBuilder<CashOutModel> builder)
        {
            builder.ToTable("CashOut");

            builder.HasKey(c => c.Id);
            builder.Property(c => c.Id)
                .HasColumnType("bigint")
                .IsRequired();

            builder.Property(c => c.Amount)
                .IsRequired();

            builder.Property(c => c.CancelationDate)
                .HasColumnType("timestamp");

            builder.Property(c => c.Date)
                .HasColumnType("timestamp")
                .IsRequired();

            builder.Property(c => c.IsActive)
                .HasColumnType("bool")
                .HasDefaultValue(true)
                .IsRequired();

            builder.Property(c => c.UpdatedDate)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}
