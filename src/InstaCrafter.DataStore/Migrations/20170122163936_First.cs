using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                "Url",
                "InstaPosts");

            migrationBuilder.AlterColumn<long>(
                "UserId",
                "InstaPosts",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                "UserId",
                "InstaPosts",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                "Url",
                "InstaPosts",
                nullable: true);
        }
    }
}