using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaCrafter.Web.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstaUsers",
                columns: table => new
                {
                    InternalUserId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    Pk = table.Column<long>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    ProfilePictureId = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaUsers", x => x.InternalUserId);
                });

            migrationBuilder.CreateTable(
                name: "InstaStories",
                columns: table => new
                {
                    InternalStoryId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CanReply = table.Column<bool>(nullable: false),
                    ExpiringAt = table.Column<DateTime>(nullable: true),
                    Id = table.Column<string>(nullable: true),
                    LatestReelMedia = table.Column<string>(nullable: true),
                    Muted = table.Column<bool>(nullable: false),
                    PrefetchCount = table.Column<int>(nullable: false),
                    RankedPosition = table.Column<int>(nullable: false),
                    Seen = table.Column<DateTime>(nullable: true),
                    SeenRankedPosition = table.Column<int>(nullable: false),
                    SocialContext = table.Column<string>(nullable: true),
                    SourceToken = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserInternalUserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaStories", x => x.InternalStoryId);
                    table.ForeignKey(
                        name: "FK_InstaStories_InstaUsers_UserInternalUserId",
                        column: x => x.UserInternalUserId,
                        principalTable: "InstaUsers",
                        principalColumn: "InternalUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "InstaPosts",
                columns: table => new
                {
                    InternalPostId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Caption = table.Column<string>(nullable: true),
                    ClientCacheKey = table.Column<string>(nullable: true),
                    Code = table.Column<string>(nullable: true),
                    CommentsCount = table.Column<string>(nullable: true),
                    DeviceTimeStap = table.Column<DateTime>(nullable: true),
                    FilterType = table.Column<string>(nullable: true),
                    HasAudio = table.Column<bool>(nullable: false),
                    HasLiked = table.Column<bool>(nullable: false),
                    Height = table.Column<string>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    InstaIdentifier = table.Column<string>(nullable: true),
                    InstaStoryInternalStoryId = table.Column<long>(nullable: true),
                    IsMultiPost = table.Column<bool>(nullable: false),
                    LikesCount = table.Column<int>(nullable: false),
                    MediaType = table.Column<int>(nullable: false),
                    PhotoOfYou = table.Column<bool>(nullable: false),
                    Pk = table.Column<string>(nullable: true),
                    TakenAt = table.Column<DateTime>(nullable: true),
                    TrakingToken = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserInternalUserId = table.Column<long>(nullable: true),
                    ViewCount = table.Column<int>(nullable: false),
                    Width = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaPosts", x => x.InternalPostId);
                    table.ForeignKey(
                        name: "FK_InstaPosts_InstaStories_InstaStoryInternalStoryId",
                        column: x => x.InstaStoryInternalStoryId,
                        principalTable: "InstaStories",
                        principalColumn: "InternalStoryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_InstaPosts_InstaUsers_UserInternalUserId",
                        column: x => x.UserInternalUserId,
                        principalTable: "InstaUsers",
                        principalColumn: "InternalUserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_InstaPosts_InstaStoryInternalStoryId",
                table: "InstaPosts",
                column: "InstaStoryInternalStoryId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaPosts_UserInternalUserId",
                table: "InstaPosts",
                column: "UserInternalUserId");

            migrationBuilder.CreateIndex(
                name: "IX_InstaStories_UserInternalUserId",
                table: "InstaStories",
                column: "UserInternalUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstaPosts");

            migrationBuilder.DropTable(
                name: "InstaStories");

            migrationBuilder.DropTable(
                name: "InstaUsers");
        }
    }
}
