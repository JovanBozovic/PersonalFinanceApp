using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace PersonalFinanceApp.Migrations
{
    public partial class Migration3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SplittedTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Catcode = table.Column<string>(type: "text", nullable: true),
                    Amount = table.Column<double>(type: "double precision", nullable: false),
                    TransactionId = table.Column<int>(type: "integer", nullable: true),
                    Categorycode = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SplittedTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SplittedTransactions_Categories_Categorycode",
                        column: x => x.Categorycode,
                        principalTable: "Categories",
                        principalColumn: "code");
                    table.ForeignKey(
                        name: "FK_SplittedTransactions_Transactions_TransactionId",
                        column: x => x.TransactionId,
                        principalTable: "Transactions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SplittedTransactions_Categorycode",
                table: "SplittedTransactions",
                column: "Categorycode");

            migrationBuilder.CreateIndex(
                name: "IX_SplittedTransactions_TransactionId",
                table: "SplittedTransactions",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SplittedTransactions");
        }
    }
}
