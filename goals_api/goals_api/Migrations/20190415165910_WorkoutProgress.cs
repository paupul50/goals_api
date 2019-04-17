using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class WorkoutProgress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WorkoutPointProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    WorkoutPointId = table.Column<int>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    WorkoutProgress = table.Column<int>(nullable: false),
                    RoutePointId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutPointProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutPointProgresses_RoutePoints_RoutePointId",
                        column: x => x.RoutePointId,
                        principalTable: "RoutePoints",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WorkoutProgresses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ProgressIndex = table.Column<int>(nullable: false),
                    IsDone = table.Column<bool>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkoutProgresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkoutProgresses_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutPointProgresses_RoutePointId",
                table: "WorkoutPointProgresses",
                column: "RoutePointId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkoutProgresses_WorkoutId",
                table: "WorkoutProgresses",
                column: "WorkoutId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WorkoutPointProgresses");

            migrationBuilder.DropTable(
                name: "WorkoutProgresses");
        }
    }
}
