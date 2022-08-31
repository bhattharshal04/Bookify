using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class AddUID : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductOrderViewModel_ProductOrderViewModelOrderId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Buyouts");

            migrationBuilder.DropTable(
                name: "ProductOrderViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductOrderViewModelOrderId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductOrderViewModelOrderId",
                table: "Products");

            migrationBuilder.AddColumn<Guid>(
                name: "guid",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "guid",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "ProductOrderViewModelOrderId",
                table: "Products",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Buyouts",
                columns: table => new
                {
                    OrdeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyoutTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    IsBought = table.Column<bool>(type: "bit", nullable: false),
                    IsRented = table.Column<bool>(type: "bit", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Buyouts", x => x.OrdeId);
                });

            migrationBuilder.CreateTable(
                name: "ProductOrderViewModel",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuyerId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BuyoutTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    RentEndDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RentId = table.Column<int>(type: "int", nullable: true),
                    RentStartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    SellerId = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
    }
}
