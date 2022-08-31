using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class DBChangeAgain : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ProductId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Products_OrderId",
                table: "Products");

            migrationBuilder.DropIndex(
                name: "IX_Buyouts_ProductId",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "RentId",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "TotalPrice",
                table: "Orders",
                newName: "BuyoutTotal");

            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentEndDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RentStartDate",
                table: "Orders",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsBought",
                table: "Buyouts",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RentEndDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "RentStartDate",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Buyouts");

            migrationBuilder.RenameColumn(
                name: "BuyoutTotal",
                table: "Orders",
                newName: "TotalPrice");

            migrationBuilder.AddColumn<int>(
                name: "RentId",
                table: "Orders",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ProductId",
                table: "Rentals",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_OrderId",
                table: "Products",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyouts_ProductId",
                table: "Buyouts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Orders_OrderId",
                table: "Products",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "OrderId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
