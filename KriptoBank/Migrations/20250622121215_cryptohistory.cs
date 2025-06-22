using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KriptoBank.Migrations
{
    /// <inheritdoc />
    public partial class cryptohistory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "NewPrice",
                table: "Histories",
                newName: "CurrentPrice");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CurrentPrice",
                table: "Histories",
                newName: "NewPrice");
        }
    }
}
