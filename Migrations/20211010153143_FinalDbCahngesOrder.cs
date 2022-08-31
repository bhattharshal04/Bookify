using Microsoft.EntityFrameworkCore.Migrations;

namespace BookifyNew.Migrations
{
    public partial class FinalDbCahngesOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrderId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "IsBought",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "IsRented",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "RentId",
                table: "Orders",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RentId",
                table: "Orders");

            migrationBuilder.AddColumn<int>(
                name: "OrderId",
                table: "Rentals",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
        }
    }
}
