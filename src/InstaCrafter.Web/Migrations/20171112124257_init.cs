using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaBackup.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstaUsers",
                columns: table => new
                {
                    InternalUserId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    IsPrivate = table.Column<bool>(type: "bool", nullable: false),
                    IsVerified = table.Column<bool>(type: "bool", nullable: false),
                    Pk = table.Column<long>(type: "int8", nullable: false),
                    ProfilePicture = table.Column<string>(type: "text", nullable: true),
                    ProfilePictureId = table.Column<string>(type: "text", nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaUsers", x => x.InternalUserId);
                });

            migrationBuilder.CreateTable(
                name: "InstaStories",
                columns: table => new
                {
                    InternalStoryId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CanReply = table.Column<bool>(type: "bool", nullable: false),
                    ExpiringAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    Id = table.Column<string>(type: "text", nullable: true),
                    LatestReelMedia = table.Column<string>(type: "text", nullable: true),
                    Muted = table.Column<bool>(type: "bool", nullable: false),
                    PrefetchCount = table.Column<int>(type: "int4", nullable: false),
                    RankedPosition = table.Column<int>(type: "int4", nullable: false),
                    Seen = table.Column<DateTime>(type: "timestamp", nullable: true),
                    SeenRankedPosition = table.Column<int>(type: "int4", nullable: false),
                    SocialContext = table.Column<string>(type: "text", nullable: true),
                    SourceToken = table.Column<string>(type: "text", nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserInternalUserId = table.Column<long>(type: "int8", nullable: true)
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
                    InternalPostId = table.Column<long>(type: "int8", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Caption = table.Column<string>(type: "text", nullable: true),
                    ClientCacheKey = table.Column<string>(type: "text", nullable: true),
                    Code = table.Column<string>(type: "text", nullable: true),
                    CommentsCount = table.Column<string>(type: "text", nullable: true),
                    DeviceTimeStap = table.Column<DateTime>(type: "timestamp", nullable: true),
                    FilterType = table.Column<string>(type: "text", nullable: true),
                    HasAudio = table.Column<bool>(type: "bool", nullable: false),
                    HasLiked = table.Column<bool>(type: "bool", nullable: false),
                    Height = table.Column<string>(type: "text", nullable: true),
                    ImageUrl = table.Column<string>(type: "text", nullable: true),
                    InstaIdentifier = table.Column<string>(type: "text", nullable: true),
                    InstaStoryInternalStoryId = table.Column<long>(type: "int8", nullable: true),
                    IsMultiPost = table.Column<bool>(type: "bool", nullable: false),
                    LikesCount = table.Column<int>(type: "int4", nullable: false),
                    MediaType = table.Column<int>(type: "int4", nullable: false),
                    PhotoOfYou = table.Column<bool>(type: "bool", nullable: false),
                    Pk = table.Column<string>(type: "text", nullable: true),
                    TakenAt = table.Column<DateTime>(type: "timestamp", nullable: true),
                    TrakingToken = table.Column<string>(type: "text", nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(type: "timestamp", nullable: false),
                    UserInternalUserId = table.Column<long>(type: "int8", nullable: true),
                    ViewCount = table.Column<int>(type: "int4", nullable: false),
                    Width = table.Column<int>(type: "int4", nullable: false)
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
