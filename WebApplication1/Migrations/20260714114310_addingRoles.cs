using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NoodlesEgypt.Migrations
{
    /// <inheritdoc />
    public partial class addingRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3A3B4F5E-8F61-49D3-9A57-7F74A1B1C001", null, "Admin", "ADMIN" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3A3B4F5E-8F61-49D3-9A57-7F74A1B1C001");
        }
    }
}
