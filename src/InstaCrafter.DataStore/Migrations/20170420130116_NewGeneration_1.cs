using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.DataStore.Migrations
{
    public partial class NewGeneration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_InstaPosts_InstaUsers_UserId1",
                "InstaPosts");

            migrationBuilder.DropTable(
                "InstaUser");

            migrationBuilder.RenameColumn(
                "UserId1",
                "InstaPosts",
                "UserId");

            migrationBuilder.RenameIndex(
                "IX_InstaPosts_UserId1",
                table: "InstaPosts",
                newName: "IX_InstaPosts_UserId");

            migrationBuilder.AddColumn<DateTime>(
                "UpdatedTimestamp",
                "InstaUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                "FK_InstaPosts_InstaUsers_UserId",
                "InstaPosts",
                "UserId",
                "InstaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_InstaPosts_InstaUsers_UserId",
                "InstaPosts");

            migrationBuilder.DropColumn(
                "UpdatedTimestamp",
                "InstaUsers");

            migrationBuilder.RenameColumn(
                "UserId",
                "InstaPosts",
                "UserId1");

            migrationBuilder.RenameIndex(
                "IX_InstaPosts_UserId",
                table: "InstaPosts",
                newName: "IX_InstaPosts_UserId1");

            migrationBuilder.CreateTable(
                "InstaUser",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    InstaIdentifier = table.Column<long>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_InstaUser", x => x.Id); });

            migrationBuilder.AddForeignKey(
                "FK_InstaPosts_InstaUsers_UserId1",
                "InstaPosts",
                "UserId1",
                "InstaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}