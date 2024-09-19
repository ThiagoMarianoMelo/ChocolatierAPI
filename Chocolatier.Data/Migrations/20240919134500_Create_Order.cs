using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chocolatier.Data.Migrations
{
    /// <inheritdoc />
    public partial class Create_Order : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Order",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RequestedById = table.Column<string>(type: "text", nullable: false),
                    DeadLine = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    CurrentStatus = table.Column<int>(type: "integer", nullable: false),
                    CancelReason = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Order_AspNetUsers_RequestedById",
                        column: x => x.RequestedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Order_RequestedById",
                table: "Order",
                column: "RequestedById");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Order");
        }
    }
}
