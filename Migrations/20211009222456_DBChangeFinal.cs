using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class DBChangeFinal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentQuauntity",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RentTotal",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Products");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentQuauntity",
                table: "Rentals",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RentTotal",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Products",
                type: "int",
                nullable: true);
        }
    }
}
