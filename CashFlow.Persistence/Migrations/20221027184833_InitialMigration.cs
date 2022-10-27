using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CashFlow.Persistence.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cashflow");

            migrationBuilder.CreateTable(
                name: "CashIn",
                schema: "cashflow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CancelationDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsActive = table.Column<bool>(type: "bool", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashIn", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CashOut",
                schema: "cashflow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CancelationDate = table.Column<DateTime>(type: "timestamp", nullable: true),
                    IsActive = table.Column<bool>(type: "bool", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashOut", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsolidateDay",
                schema: "cashflow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Day = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CashInAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    CashOutAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidateDay", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsolidateMonth",
                schema: "cashflow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Month = table.Column<DateTime>(type: "timestamp", nullable: false),
                    CashInAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    CashOutAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidateMonth", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ConsolidateYear",
                schema: "cashflow",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Year = table.Column<int>(type: "int", nullable: false),
                    CashInAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    CashOutAmout = table.Column<decimal>(type: "numeric", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConsolidateYear", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidateDay_Day",
                schema: "cashflow",
                table: "ConsolidateDay",
                column: "Day",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidateMonth_Month",
                schema: "cashflow",
                table: "ConsolidateMonth",
                column: "Month",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConsolidateYear_Year",
                schema: "cashflow",
                table: "ConsolidateYear",
                column: "Year",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashIn",
                schema: "cashflow");

            migrationBuilder.DropTable(
                name: "CashOut",
                schema: "cashflow");

            migrationBuilder.DropTable(
                name: "ConsolidateDay",
                schema: "cashflow");

            migrationBuilder.DropTable(
                name: "ConsolidateMonth",
                schema: "cashflow");

            migrationBuilder.DropTable(
                name: "ConsolidateYear",
                schema: "cashflow");
        }
    }
}
