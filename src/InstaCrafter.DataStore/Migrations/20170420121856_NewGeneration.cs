﻿using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.DataStore.Migrations
{
    public partial class NewGeneration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                "InstaUsers",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ExternalUrl = table.Column<string>(nullable: true),
                    FollowedByCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    InstaIdentifier = table.Column<long>(nullable: false),
                    IsVerified = table.Column<string>(nullable: true),
                    ProfilePicture = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_InstaUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "InstaPosts",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Code = table.Column<string>(nullable: true),
                    CreatedTime = table.Column<DateTime>(nullable: false),
                    ImageLink = table.Column<string>(nullable: true),
                    Link = table.Column<string>(nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(nullable: false),
                    UserId1 = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstaPosts", x => x.Id);
                    table.ForeignKey(
                        "FK_InstaPosts_InstaUsers_UserId1",
                        x => x.UserId1,
                        "InstaUsers",
                        "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                "IX_InstaPosts_UserId1",
                "InstaPosts",
                "UserId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "InstaUser");

            migrationBuilder.DropTable(
                "InstaPosts");

            migrationBuilder.DropTable(
                "InstaUsers");
        }
    }
}