using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaCrafter.Web.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Caption",
                table: "InstaPosts");

            migrationBuilder.AddColumn<long>(
                name: "CaptionInternalCaptionId",
                table: "InstaPosts",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "InstaCaptions",
                columns: table => new
                {
                    InternalCaptionId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(nullable: false),
                    MediaId = table.Column<string>(nullable: true),
                    Pk = table.Column<string>(nullable: true),
                    Text = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserInternalUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaCaptions", x => x.InternalCaptionId);
                    table.ForeignKey(
                        name: "FK_InstaCaptions_InstaUsers_UserInternalUserId",
                        column: x => x.UserInternalUserId,
                        principalTable: "InstaUsers",
                        principalColumn: "InternalUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstaCarouselItems",
                columns: table => new
                {
                    InternalCarouselItemId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CarouselParentId = table.Column<string>(nullable: true),
                    Height = table.Column<int>(nullable: false),
                    InstaIdentifier = table.Column<string>(nullable: true),
                    InstaMediaPostInternalPostId = table.Column<long>(nullable: true),
                    Pk = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaCarouselItems", x => x.InternalCarouselItemId);
                    table.ForeignKey(
                        name: "FK_InstaCarouselItems_InstaPosts_InstaMediaPostInternalPostId",
                        column: x => x.InstaMediaPostInternalPostId,
                        principalTable: "InstaPosts",
                        principalColumn: "InternalPostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstaMedias",
                columns: table => new
                {
                    InternalMediaId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Height = table.Column<int>(nullable: false),
                    InstaCarouselItemInternalCarouselItemId = table.Column<long>(nullable: true),
                    InstaMediaPostInternalPostId = table.Column<long>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    Url = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaMedias", x => x.InternalMediaId);
                    table.ForeignKey(
                        name: "FK_InstaMedias_InstaCarouselItems_InstaCarouselItemInternalCarouselItemId",
                        column: x => x.InstaCarouselItemInternalCarouselItemId,
                        principalTable: "InstaCarouselItems",
                        principalColumn: "InternalCarouselItemId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstaMedias_InstaPosts_InstaMediaPostInternalPostId",
                        column: x => x.InstaMediaPostInternalPostId,
                        principalTable: "InstaPosts",
                        principalColumn: "InternalPostId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstaPosts_CaptionInternalCaptionId",
                table: "InstaPosts",
                column: "CaptionInternalCaptionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaCaptions_UserInternalUserId",
                table: "InstaCaptions",
                column: "UserInternalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaCarouselItems_InstaMediaPostInternalPostId",
                table: "InstaCarouselItems",
                column: "InstaMediaPostInternalPostId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaMedias_InstaCarouselItemInternalCarouselItemId",
                table: "InstaMedias",
                column: "InstaCarouselItemInternalCarouselItemId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaMedias_InstaMediaPostInternalPostId",
                table: "InstaMedias",
                column: "InstaMediaPostInternalPostId");

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_InstaCaptions_CaptionInternalCaptionId",
                table: "InstaPosts",
                column: "CaptionInternalCaptionId",
                principalTable: "InstaCaptions",
                principalColumn: "InternalCaptionId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_InstaCaptions_CaptionInternalCaptionId",
                table: "InstaPosts");

            migrationBuilder.DropTable(
                name: "InstaCaptions");

            migrationBuilder.DropTable(
                name: "InstaMedias");

            migrationBuilder.DropTable(
                name: "InstaCarouselItems");

            migrationBuilder.DropIndex(
                name: "IX_InstaPosts_CaptionInternalCaptionId",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "CaptionInternalCaptionId",
                table: "InstaPosts");

            migrationBuilder.AddColumn<string>(
                name: "Caption",
                table: "InstaPosts",
                nullable: true);
        }
    }
}
