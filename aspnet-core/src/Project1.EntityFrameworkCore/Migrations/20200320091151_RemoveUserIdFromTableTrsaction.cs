using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.Migrations
{
    public partial class RemoveUserIdFromTableTrsaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Test.Transaction");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Test.Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
