using Microsoft.EntityFrameworkCore.Migrations;

namespace goals_api.Migrations
{
    public partial class CommentUpgrade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "username",
                table: "Comments",
                newName: "Username");

            migrationBuilder.AlterColumn<string>(
                name: "Username",
                table: "Comments",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CommentUserDescriptionId",
                table: "Comments",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentUserDescriptionId",
                table: "Comments");

            migrationBuilder.RenameColumn(
                name: "Username",
                table: "Comments",
                newName: "username");

            migrationBuilder.AlterColumn<string>(
                name: "username",
                table: "Comments",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
