using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalFinanceApp.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    code = table.Column<string>(type: "text", nullable: false),
                    parent_code = table.Column<string>(type: "text", nullable: true),
                    name = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.code);
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Beneficiary_Name = table.Column<string>(type: "text", nullable: true),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Direction = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<float>(type: "real", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true),
                    Currency = table.Column<string>(type: "text", nullable: true),
                    Mcc = table.Column<int>(type: "integer", nullable: true),
                    Kind = table.Column<string>(type: "text", nullable: true),
                    Catcode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Transactions");
        }
    }
}
