using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.Migrations
{
    public partial class fixtypeimage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Test.Car");

            migrationBuilder.AddColumn<string>(
                name: "CarImage",
                table: "Test.Car",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CarImage",
                table: "Test.Car");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Test.Car",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);
        }
    }
}
