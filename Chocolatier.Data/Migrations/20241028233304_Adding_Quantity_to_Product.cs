using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Adding_Quantity_to_Product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "SaleItem",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Product",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "SaleItem");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Product");
        }
    }
}
