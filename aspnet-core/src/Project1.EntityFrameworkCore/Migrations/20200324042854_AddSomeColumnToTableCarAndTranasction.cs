using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.Migrations
{
    public partial class AddSomeColumnToTableCarAndTranasction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Test.Transaction",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Test.Transaction",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "Inventory",
                table: "Test.Car",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Test.Car",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Test.Car",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Test.Transaction");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Test.Transaction");

            migrationBuilder.DropColumn(
                name: "Inventory",
                table: "Test.Car");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Test.Car");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Test.Car");
        }
    }
}
