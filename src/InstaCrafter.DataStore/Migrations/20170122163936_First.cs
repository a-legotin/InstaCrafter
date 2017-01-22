using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "InstaPosts");

            migrationBuilder.AlterColumn<long>(
                name: "UserId",
                table: "InstaPosts",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "InstaPosts",
                nullable: false);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "InstaPosts",
                nullable: true);
        }
    }
}
