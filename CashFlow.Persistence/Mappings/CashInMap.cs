using CashFlow.Domain.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Persistence.Mappings
{
    public class CashInMap : IEntityTypeConfiguration<CashInModel>
    {
        public void Configure(EntityTypeBuilder<CashInModel> builder)
        {
            builder.ToTable("CashIn");

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
