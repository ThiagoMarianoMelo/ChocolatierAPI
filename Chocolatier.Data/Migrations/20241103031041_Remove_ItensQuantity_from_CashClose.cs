using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Remove_ItensQuantity_from_CashClose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItensQuantity",
                table: "CashClose");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ItensQuantity",
                table: "CashClose",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
