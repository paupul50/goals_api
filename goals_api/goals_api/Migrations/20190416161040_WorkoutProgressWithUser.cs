using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class WorkoutProgressWithUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Username",
                table: "WorkoutProgresses",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgresses_Username",
                table: "WorkoutProgresses",
                column: "Username");

            migrationBuilder.AddForeignKey(
                name: "FK_WorkoutProgresses_Users_Username",
                table: "WorkoutProgresses",
                column: "Username",
                principalTable: "Users",
                principalColumn: "Username",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_WorkoutProgresses_Users_Username",
                table: "WorkoutProgresses");

            migrationBuilder.DropIndex(
                name: "IX_WorkoutProgresses_Username",
                table: "WorkoutProgresses");

            migrationBuilder.DropColumn(
                name: "Username",
                table: "WorkoutProgresses");
        }
    }
}
