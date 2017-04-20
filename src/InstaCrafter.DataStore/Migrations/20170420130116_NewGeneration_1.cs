using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InstaCrafter.DataStore.Migrations
{
    public partial class NewGeneration_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_InstaUsers_UserId1",
                table: "InstaPosts");

            migrationBuilder.DropTable(
                name: "InstaUser");

            migrationBuilder.RenameColumn(
                name: "UserId1",
                table: "InstaPosts",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_InstaPosts_UserId1",
                table: "InstaPosts",
                newName: "IX_InstaPosts_UserId");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedTimestamp",
                table: "InstaUsers",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_InstaUsers_UserId",
                table: "InstaPosts",
                column: "UserId",
                principalTable: "InstaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_InstaUsers_UserId",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "UpdatedTimestamp",
                table: "InstaUsers");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "InstaPosts",
                newName: "UserId1");

            migrationBuilder.RenameIndex(
                name: "IX_InstaPosts_UserId",
                table: "InstaPosts",
                newName: "IX_InstaPosts_UserId1");

            migrationBuilder.CreateTable(
                name: "InstaUser",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    InstaIdentifier = table.Column<long>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaUser", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_InstaUsers_UserId1",
                table: "InstaPosts",
                column: "UserId1",
                principalTable: "InstaUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
