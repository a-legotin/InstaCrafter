using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InstaCrafter.PostService.Migrations
{
    public partial class initialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstaPosts",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TakenAt = table.Column<DateTime>(nullable: false),
                    Pk = table.Column<string>(nullable: true),
                    InstaIdentifier = table.Column<string>(nullable: true),
                    DeviceTimeStamp = table.Column<DateTime>(nullable: false),
                    MediaType = table.Column<int>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ClientCacheKey = table.Column<string>(nullable: true),
                    FilterType = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<string>(nullable: true),
                    TrackingToken = table.Column<string>(nullable: true),
                    LikesCount = table.Column<int>(nullable: false),
                    NextMaxId = table.Column<string>(nullable: true),
                    CommentsCount = table.Column<string>(nullable: true),
                    PhotoOfYou = table.Column<bool>(nullable: false),
                    HasLiked = table.Column<bool>(nullable: false),
                    ViewCount = table.Column<int>(nullable: false),
                    HasAudio = table.Column<bool>(nullable: false),
                    UserId = table.Column<long>(nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaPosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstaImages",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<long>(nullable: true),
                    URI = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaImages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstaImages_InstaPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "InstaPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstaVideos",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<long>(nullable: true),
                    Url = table.Column<string>(nullable: true),
                    Path = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaVideos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstaVideos_InstaPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "InstaPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstaImages_PostId",
                table: "InstaImages",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaVideos_PostId",
                table: "InstaVideos",
                column: "PostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstaImages");

            migrationBuilder.DropTable(
                name: "InstaVideos");

            migrationBuilder.DropTable(
                name: "InstaPosts");
        }
    }
}
