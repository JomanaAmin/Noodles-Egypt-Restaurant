using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication1.Migrations
{
    /// <inheritdoc />
    public partial class SecondMmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VariantPrice",
                table: "ProductVariants",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "VariantName",
                table: "ProductVariants",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "VariantDescription",
                table: "ProductVariants",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ProductVariantId",
                table: "ProductVariants",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "Products",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "Products",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "Products",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Categories",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "CategoryDescription",
                table: "Categories",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Categories",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "Products",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "Products");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "ProductVariants",
                newName: "VariantPrice");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "ProductVariants",
                newName: "VariantName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "ProductVariants",
                newName: "VariantDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ProductVariants",
                newName: "ProductVariantId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Products",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Products",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Products",
                newName: "ProductId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Categories",
                newName: "CategoryName");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Categories",
                newName: "CategoryDescription");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Categories",
                newName: "CategoryId");
        }
    }
}
