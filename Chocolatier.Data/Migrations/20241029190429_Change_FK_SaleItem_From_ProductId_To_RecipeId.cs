using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Change_FK_SaleItem_From_ProductId_To_RecipeId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItem_Product_ProductId",
                table: "SaleItem");

            migrationBuilder.RenameColumn(
                name: "ProductId",
                table: "SaleItem",
                newName: "RecipeId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItem_ProductId",
                table: "SaleItem",
                newName: "IX_SaleItem_RecipeId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItem_Recipe_RecipeId",
                table: "SaleItem",
                column: "RecipeId",
                principalTable: "Recipe",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SaleItem_Recipe_RecipeId",
                table: "SaleItem");

            migrationBuilder.RenameColumn(
                name: "RecipeId",
                table: "SaleItem",
                newName: "ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_SaleItem_RecipeId",
                table: "SaleItem",
                newName: "IX_SaleItem_ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_SaleItem_Product_ProductId",
                table: "SaleItem",
                column: "ProductId",
                principalTable: "Product",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
