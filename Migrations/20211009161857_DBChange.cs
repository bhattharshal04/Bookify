using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class DBChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Buyouts_ProductViewModel_ProductViewModelProductId",
                table: "Buyouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_ProductViewModel_ProductViewModelProductId",
                table: "Rentals");

            migrationBuilder.DropTable(
                name: "ProductViewModel");

            migrationBuilder.DropTable(
                name: "TwoFactorViewModel");

            migrationBuilder.DropTable(
                name: "UserLoginViewModel");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_ProductViewModelProductId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Buyouts_ProductViewModelProductId",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "ProductViewModelProductId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "BuyoutQuauntity",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "ProductViewModelProductId",
                table: "Buyouts");

            migrationBuilder.RenameColumn(
                name: "BuyoutStartDate",
                table: "Buyouts",
                newName: "RentStartDate");

            migrationBuilder.RenameColumn(
                name: "BuyoutEndDate",
                table: "Buyouts",
                newName: "RentEndDate");

            migrationBuilder.RenameColumn(
                name: "BuyoutId",
                table: "Buyouts",
                newName: "OrdeId");

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Buyouts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyoutTotal",
                table: "Buyouts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BuyerId",
                table: "Buyouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRented",
                table: "Buyouts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "OrderDate",
                table: "Buyouts",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "SellerId",
                table: "Buyouts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "BuyerId",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "OrderDate",
                table: "Buyouts");

            migrationBuilder.DropColumn(
                name: "SellerId",
                table: "Buyouts");

            migrationBuilder.RenameColumn(
                name: "RentStartDate",
                table: "Buyouts",
                newName: "BuyoutStartDate");

            migrationBuilder.RenameColumn(
                name: "RentEndDate",
                table: "Buyouts",
                newName: "BuyoutEndDate");

            migrationBuilder.RenameColumn(
                name: "OrdeId",
                table: "Buyouts",
                newName: "BuyoutId");

            migrationBuilder.AddColumn<int>(
                name: "ProductViewModelProductId",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Orders",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Buyouts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "BuyoutTotal",
                table: "Buyouts",
                type: "decimal(18,2)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddColumn<int>(
                name: "BuyoutQuauntity",
                table: "Buyouts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductViewModelProductId",
                table: "Buyouts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductViewModel",
                columns: table => new
                {
                    ProductId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    ImageName = table.Column<string>(type: "varchar(50)", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(250)", nullable: true),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    IsRentalAvailable = table.Column<bool>(type: "bit", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ProductIndexDisplay = table.Column<int>(type: "int", nullable: false),
                    ProductLongDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    ProductShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductViewModel", x => x.ProductId);
                    table.ForeignKey(
                        name: "FK_ProductViewModel_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductViewModel_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "OrderId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TwoFactorViewModel",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TwoFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "UserLoginViewModel",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_ProductViewModelProductId",
                table: "Rentals",
                column: "ProductViewModelProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Buyouts_ProductViewModelProductId",
                table: "Buyouts",
                column: "ProductViewModelProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewModel_CategoryId",
                table: "ProductViewModel",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductViewModel_OrderId",
                table: "ProductViewModel",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buyouts_Products_ProductId",
                table: "Buyouts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Buyouts_ProductViewModel_ProductViewModelProductId",
                table: "Buyouts",
                column: "ProductViewModelProductId",
                principalTable: "ProductViewModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_ProductViewModel_ProductViewModelProductId",
                table: "Rentals",
                column: "ProductViewModelProductId",
                principalTable: "ProductViewModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
