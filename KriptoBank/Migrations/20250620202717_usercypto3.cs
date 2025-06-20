using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KriptoBank.Migrations
{
    /// <inheritdoc />
    public partial class usercypto3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_CryptoCurrencies_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "CurrencyId",
                table: "Transactions");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CurrencyId",
                table: "Transactions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CurrencyId",
                table: "Transactions",
                column: "CurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_CryptoCurrencies_CurrencyId",
                table: "Transactions",
                column: "CurrencyId",
                principalTable: "CryptoCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
