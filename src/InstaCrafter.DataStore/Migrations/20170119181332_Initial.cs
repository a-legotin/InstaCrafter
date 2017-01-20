using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "InstaUsers",
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
                constraints: table => { table.PrimaryKey("PK_InstaUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "InstaPosts",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CanViewComment = table.Column<bool>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    Link = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    UserId = table.Column<int>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_InstaPosts", x => x.Id); });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "InstaUsers");

            migrationBuilder.DropTable(
                "InstaPosts");
        }
    }
}