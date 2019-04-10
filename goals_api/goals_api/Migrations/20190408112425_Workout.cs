using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class Workout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Workouts",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatorUsername = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Workouts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Workouts_Users_CreatorUsername",
                        column: x => x.CreatorUsername,
                        principalTable: "Users",
                        principalColumn: "Username",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RoutePoints",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false),
                    Radius = table.Column<double>(nullable: false),
                    FillColour = table.Column<string>(nullable: true),
                    CircleDraggable = table.Column<bool>(nullable: false),
                    Editable = table.Column<bool>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    WorkoutId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoutePoints", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoutePoints_Workouts_WorkoutId",
                        column: x => x.WorkoutId,
                        principalTable: "Workouts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RoutePoints_WorkoutId",
                table: "RoutePoints",
                column: "WorkoutId");

            migrationBuilder.CreateIndex(
                name: "IX_Workouts_CreatorUsername",
                table: "Workouts",
                column: "CreatorUsername");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoutePoints");

            migrationBuilder.DropTable(
                name: "Workouts");
        }
    }
}
