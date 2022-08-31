using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class AddPostDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PostDate",
                table: "Products",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderViewModelOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductOrderViewModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    BuyoutTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductOrderViewModel", x => x.OrderId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductOrderViewModelOrderId",
                table: "Products",
                column: "ProductOrderViewModelOrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductOrderViewModel_ProductOrderViewModelOrderId",
                table: "Products",
                column: "ProductOrderViewModelOrderId",
                principalTable: "ProductOrderViewModel",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductOrderViewModel_ProductOrderViewModelOrderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductOrderViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductOrderViewModelOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "PostDate",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductOrderViewModelOrderId",
                table: "Products");
        }
    }
}
