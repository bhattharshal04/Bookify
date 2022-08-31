using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class ChangeProductId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProductViewModelProductId",
                table: "Rentals",
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
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductShortDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductLongDesc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageName = table.Column<string>(type: "varchar(50)", nullable: true),
                    ImagePath = table.Column<string>(type: "varchar(250)", nullable: true),
                    ProductPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    CategoryId = table.Column<int>(type: "int", nullable: true),
                    IsRentalAvailable = table.Column<bool>(type: "bit", nullable: false),
                    IsNew = table.Column<bool>(type: "bit", nullable: false),
                    ProductIndexDisplay = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
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
                    TwoFactorCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "FK_Buyouts_ProductViewModel_ProductViewModelProductId",
                table: "Buyouts",
                column: "ProductViewModelProductId",
                principalTable: "ProductViewModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_ProductViewModel_ProductViewModelProductId",
                table: "Rentals",
                column: "ProductViewModelProductId",
                principalTable: "ProductViewModel",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buyouts_ProductViewModel_ProductViewModelProductId",
                table: "Buyouts");

            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals");

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
                name: "ProductViewModelProductId",
                table: "Buyouts");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "Rentals",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_Products_ProductId",
                table: "Rentals",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "ProductId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
