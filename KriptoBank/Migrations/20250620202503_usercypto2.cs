using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KriptoBank.Migrations
{
    /// <inheritdoc />
    public partial class usercypto2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_userCryptoCurrencies_CryptoCurrencies_CryptoCurrencyId",
                table: "userCryptoCurrencies");

            migrationBuilder.DropIndex(
                name: "IX_userCryptoCurrencies_CryptoCurrencyId",
                table: "userCryptoCurrencies");

            migrationBuilder.DropColumn(
                name: "CryptoCurrencyId",
                table: "userCryptoCurrencies");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CryptoCurrencyId",
                table: "userCryptoCurrencies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_userCryptoCurrencies_CryptoCurrencyId",
                table: "userCryptoCurrencies",
                column: "CryptoCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_userCryptoCurrencies_CryptoCurrencies_CryptoCurrencyId",
                table: "userCryptoCurrencies",
                column: "CryptoCurrencyId",
                principalTable: "CryptoCurrencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
