using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InstaCrafter.PostService.Migrations
{
    public partial class locationcarouseladded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "CarouselId",
                table: "InstaVideos",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CaptionId",
                table: "InstaPosts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "LocationId",
                table: "InstaPosts",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "CarouselId",
                table: "InstaImages",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstagramCaptionDto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    UserId = table.Column<long>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    MediaId = table.Column<string>(nullable: true),
                    Pk = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramCaptionDto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InstagramCarouselItemDto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PostId = table.Column<long>(nullable: true),
                    InstaIdentifier = table.Column<string>(nullable: true),
                    MediaType = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Pk = table.Column<string>(nullable: true),
                    CarouselParentId = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramCarouselItemDto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InstagramCarouselItemDto_InstaPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "InstaPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstagramLocationDto",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FacebookPlacesId = table.Column<long>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    Pk = table.Column<long>(nullable: false),
                    ShortName = table.Column<string>(nullable: true),
                    ExternalSource = table.Column<string>(nullable: true),
                    ExternalId = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    Lng = table.Column<double>(nullable: false),
                    Lat = table.Column<double>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstagramLocationDto", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstaVideos_CarouselId",
                table: "InstaVideos",
                column: "CarouselId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaPosts_CaptionId",
                table: "InstaPosts",
                column: "CaptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaPosts_LocationId",
                table: "InstaPosts",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaImages_CarouselId",
                table: "InstaImages",
                column: "CarouselId");

            migrationBuilder.CreateIndex(
                name: "IX_InstagramCarouselItemDto_PostId",
                table: "InstagramCarouselItemDto",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstaImages_InstagramCarouselItemDto_CarouselId",
                table: "InstaImages",
                column: "CarouselId",
                principalTable: "InstagramCarouselItemDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_InstagramCaptionDto_CaptionId",
                table: "InstaPosts",
                column: "CaptionId",
                principalTable: "InstagramCaptionDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_InstagramLocationDto_LocationId",
                table: "InstaPosts",
                column: "LocationId",
                principalTable: "InstagramLocationDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaVideos_InstagramCarouselItemDto_CarouselId",
                table: "InstaVideos",
                column: "CarouselId",
                principalTable: "InstagramCarouselItemDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstaImages_InstagramCarouselItemDto_CarouselId",
                table: "InstaImages");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_InstagramCaptionDto_CaptionId",
                table: "InstaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_InstagramLocationDto_LocationId",
                table: "InstaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaVideos_InstagramCarouselItemDto_CarouselId",
                table: "InstaVideos");

            migrationBuilder.DropTable(
                name: "InstagramCaptionDto");

            migrationBuilder.DropTable(
                name: "InstagramCarouselItemDto");

            migrationBuilder.DropTable(
                name: "InstagramLocationDto");

            migrationBuilder.DropIndex(
                name: "IX_InstaVideos_CarouselId",
                table: "InstaVideos");

            migrationBuilder.DropIndex(
                name: "IX_InstaPosts_CaptionId",
                table: "InstaPosts");

            migrationBuilder.DropIndex(
                name: "IX_InstaPosts_LocationId",
                table: "InstaPosts");

            migrationBuilder.DropIndex(
                name: "IX_InstaImages_CarouselId",
                table: "InstaImages");

            migrationBuilder.DropColumn(
                name: "CarouselId",
                table: "InstaVideos");

            migrationBuilder.DropColumn(
                name: "CaptionId",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "CarouselId",
                table: "InstaImages");
        }
    }
}
