using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace InstaCrafter.UserService.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InstaUsers",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    HasAnonymousProfilePicture = table.Column<bool>(nullable: false),
                    FollowersCount = table.Column<int>(nullable: false),
                    FollowersCountByLine = table.Column<string>(nullable: true),
                    SocialContext = table.Column<string>(nullable: true),
                    SearchSocialContext = table.Column<string>(nullable: true),
                    MutualFollowers = table.Column<int>(nullable: false),
                    UnseenCount = table.Column<int>(nullable: false),
                    IsVerified = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    Pk = table.Column<long>(nullable: false),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaUsers", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InstaUsers");
        }
    }
}
