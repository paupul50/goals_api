using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class GoogleToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GoogleStatus",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GoogleToken",
                table: "Users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoogleStatus",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "GoogleToken",
                table: "Users");
        }
    }
}
