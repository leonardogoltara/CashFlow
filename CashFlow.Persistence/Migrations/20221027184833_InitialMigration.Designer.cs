﻿// <auto-generated />
using System;
using CashFlow.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashFlow.Persistence.Migrations
{
    [DbContext(typeof(CashFlowDataContext))]
    [Migration("20221027184833_InitialMigration")]
    partial class InitialMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("cashflow")
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CashFlow.Domain.Models.CashInModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("CancelationDate")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("CashIn", "cashflow");
                });

            modelBuilder.Entity("CashFlow.Domain.Models.CashOutModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("Amount")
                        .HasColumnType("numeric");

                    b.Property<DateTime?>("CancelationDate")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("Date")
                        .HasColumnType("timestamp");

                    b.Property<bool>("IsActive")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bool")
                        .HasDefaultValue(true);

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.ToTable("CashOut", "cashflow");
                });

            modelBuilder.Entity("CashFlow.Domain.Models.ConsolidateDayModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("CashInAmout")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CashOutAmout")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Day")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("Day")
                        .IsUnique();

                    b.ToTable("ConsolidateDay", "cashflow");
                });

            modelBuilder.Entity("CashFlow.Domain.Models.ConsolidateMonthModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("CashInAmout")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CashOutAmout")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("Month")
                        .HasColumnType("timestamp");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp");

                    b.HasKey("Id");

                    b.HasIndex("Month")
                        .IsUnique();

                    b.ToTable("ConsolidateMonth", "cashflow");
                });

            modelBuilder.Entity("CashFlow.Domain.Models.ConsolidateYearModel", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<decimal>("CashInAmout")
                        .HasColumnType("numeric");

                    b.Property<decimal>("CashOutAmout")
                        .HasColumnType("numeric");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("timestamp");

                    b.Property<int>("Year")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Year")
                        .IsUnique();

                    b.ToTable("ConsolidateYear", "cashflow");
                });
#pragma warning restore 612, 618
        }
    }
}
