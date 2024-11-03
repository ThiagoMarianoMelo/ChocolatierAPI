using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_CashClose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CashClose",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    SaleQuantity = table.Column<int>(type: "integer", nullable: false),
                    ItensQuantity = table.Column<int>(type: "integer", nullable: false),
                    Billing = table.Column<double>(type: "double precision", nullable: false),
                    Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    EstablishmentId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CashClose", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CashClose_AspNetUsers_EstablishmentId",
                        column: x => x.EstablishmentId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CashClose_EstablishmentId",
                table: "CashClose",
                column: "EstablishmentId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CashClose");
        }
    }
}
