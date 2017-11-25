using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace InstaCrafter.Web.Migrations
{
    public partial class third : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "InstaPosts");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "InstaPosts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Height",
                table: "InstaPosts",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "InstaPosts",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "InstaPosts",
                nullable: false,
                defaultValue: 0);
        }
    }
}
