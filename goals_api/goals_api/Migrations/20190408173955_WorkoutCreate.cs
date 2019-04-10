using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class WorkoutCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "GroupGoals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "GroupGoals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "GoalType",
                table: "Goals",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "WorkoutId",
                table: "Goals",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "GroupGoals");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "GroupGoals");

            migrationBuilder.DropColumn(
                name: "GoalType",
                table: "Goals");

            migrationBuilder.DropColumn(
                name: "WorkoutId",
                table: "Goals");
        }
    }
}
