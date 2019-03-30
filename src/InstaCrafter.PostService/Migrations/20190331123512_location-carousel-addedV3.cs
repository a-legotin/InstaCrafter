using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.PostService.Migrations
{
    public partial class locationcarouseladdedV3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarouselItems_InstaPosts_PostId",
                table: "CarouselItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_InstaPosts_PostId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_Captions_CaptionId",
                table: "InstaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaPosts_Locations_LocationId",
                table: "InstaPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_InstaPosts_PostId",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstaPosts",
                table: "InstaPosts");

            migrationBuilder.RenameTable(
                name: "InstaPosts",
                newName: "Posts");

            migrationBuilder.RenameIndex(
                name: "IX_InstaPosts_LocationId",
                table: "Posts",
                newName: "IX_Posts_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_InstaPosts_CaptionId",
                table: "Posts",
                newName: "IX_Posts_CaptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarouselItems_Posts_PostId",
                table: "CarouselItems",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Captions_CaptionId",
                table: "Posts",
                column: "CaptionId",
                principalTable: "Captions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Locations_LocationId",
                table: "Posts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_Posts_PostId",
                table: "Videos",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarouselItems_Posts_PostId",
                table: "CarouselItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Posts_PostId",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Captions_CaptionId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Locations_LocationId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_Posts_PostId",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "InstaPosts");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_LocationId",
                table: "InstaPosts",
                newName: "IX_InstaPosts_LocationId");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_CaptionId",
                table: "InstaPosts",
                newName: "IX_InstaPosts_CaptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstaPosts",
                table: "InstaPosts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarouselItems_InstaPosts_PostId",
                table: "CarouselItems",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_InstaPosts_PostId",
                table: "Images",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_Captions_CaptionId",
                table: "InstaPosts",
                column: "CaptionId",
                principalTable: "Captions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaPosts_Locations_LocationId",
                table: "InstaPosts",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Videos_InstaPosts_PostId",
                table: "Videos",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
