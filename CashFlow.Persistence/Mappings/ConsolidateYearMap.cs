using CashFlow.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashFlow.Persistence.Mappings
{
    public class ConsolidateYearMap : IEntityTypeConfiguration<ConsolidateYearModel>
    {
        public void Configure(EntityTypeBuilder<ConsolidateYearModel> builder)
        {
            builder.ToTable("ConsolidateYear");
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasColumnType("bigint")
                .IsRequired();

            builder.HasIndex(c => c.Year).IsUnique();

            builder.Property(c => c.Year)
                .HasColumnType("int")
                .IsRequired();

            builder.Property(c => c.CashInAmout).IsRequired();
            builder.Property(c => c.CashOutAmout).IsRequired();

            builder.Property(c => c.UpdatedDate)
                .HasColumnType("timestamp")
                .IsRequired();
        }
    }
}