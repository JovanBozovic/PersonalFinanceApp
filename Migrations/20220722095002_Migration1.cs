using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalFinanceApp.Migrations
{
    public partial class Migration1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "categorycode",
                table: "Transactions",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_categorycode",
                table: "Transactions",
                column: "categorycode");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_categorycode",
                table: "Transactions",
                column: "categorycode",
                principalTable: "Categories",
                principalColumn: "code");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_categorycode",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_categorycode",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "categorycode",
                table: "Transactions");
        }
    }
}
