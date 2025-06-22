using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace KriptoBank.Migrations
{
    /// <inheritdoc />
    public partial class hopefullyfinal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "CryptoCurrencies",
                columns: new[] { "Id", "Acronym", "AvgPrice", "CurrentPrice", "IsDeleted", "Name", "TotalAmount" },
                values: new object[,]
                {
                    { 1, "BTC", 98492f, 98492f, false, "Bitcoin", 8465 },
                    { 2, "ETH", 2139.21f, 2139.21f, false, "Ethereum", 9362 },
                    { 3, "BNB", 602.99f, 602.99f, false, "Binance Coin", 4218 },
                    { 4, "XRP", 1.94f, 1.94f, false, "XRP", 2941 },
                    { 5, "USDT", 1f, 1f, false, "Tether (Polygon)", 6157 },
                    { 6, "ADA", 0.515608f, 0.515608f, false, "Cardano", 1550 },
                    { 7, "SOL", 126.87f, 126.87f, false, "Solana", 9822 },
                    { 8, "DOGE", 0.14508f, 0.14508f, false, "Dogecoin", 2774 },
                    { 9, "MATIC", 0.165074f, 0.165074f, false, "Polygon", 6733 },
                    { 10, "DOT", 3.08f, 3.08f, false, "Polkadot", 7294 },
                    { 11, "AVAX", 15.76f, 15.76f, false, "Avalanche", 1890 },
                    { 12, "LINK", 11.05f, 11.05f, false, "Chainlink", 3933 },
                    { 13, "LTC", 77.01f, 77.01f, false, "Litecoin", 5527 },
                    { 14, "SHIB", 1.017E-05f, 1.017E-05f, false, "Shiba Inu", 1084 },
                    { 15, "TRX", 0.26054f, 0.26054f, false, "TRON", 7871 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "IsDeleted", "Password", "Username" },
                values: new object[,]
                {
                    { 1, "user1@example.com", false, "user11", "user1" },
                    { 2, "user2@example.com", false, "user22", "user2" },
                    { 3, "user3@example.com", false, "user33", "user3" }
                });

            migrationBuilder.InsertData(
                table: "Wallets",
                columns: new[] { "Id", "Balance", "IsDeleted", "UserId" },
                values: new object[,]
                {
                    { 1, 4000f, false, 1 },
                    { 2, 4000f, false, 2 },
                    { 3, 4000f, false, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "CryptoCurrencies",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Wallets",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
