using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Fixing_TablesName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                table: "Ingredients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientTypes",
                table: "IngredientTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients");

            migrationBuilder.RenameTable(
                name: "IngredientTypes",
                newName: "IngredientType");

            migrationBuilder.RenameTable(
                name: "Ingredients",
                newName: "Ingredient");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredients_IngredientTypeId",
                table: "Ingredient",
                newName: "IX_Ingredient_IngredientTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientType",
                table: "IngredientType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredient_IngredientType_IngredientTypeId",
                table: "Ingredient",
                column: "IngredientTypeId",
                principalTable: "IngredientType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ingredient_IngredientType_IngredientTypeId",
                table: "Ingredient");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IngredientType",
                table: "IngredientType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Ingredient",
                table: "Ingredient");

            migrationBuilder.RenameTable(
                name: "IngredientType",
                newName: "IngredientTypes");

            migrationBuilder.RenameTable(
                name: "Ingredient",
                newName: "Ingredients");

            migrationBuilder.RenameIndex(
                name: "IX_Ingredient_IngredientTypeId",
                table: "Ingredients",
                newName: "IX_Ingredients_IngredientTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IngredientTypes",
                table: "IngredientTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Ingredients",
                table: "Ingredients",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                table: "Ingredients",
                column: "IngredientTypeId",
                principalTable: "IngredientTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
