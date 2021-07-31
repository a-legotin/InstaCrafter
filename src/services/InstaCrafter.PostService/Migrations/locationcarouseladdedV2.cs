using Microsoft.EntityFrameworkCore.Migrations;

namespace InstaCrafter.PostService.Migrations
{
    public partial class locationcarouseladdedV2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InstagramCarouselItemDto_InstaPosts_PostId",
                table: "InstagramCarouselItemDto");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaImages_InstagramCarouselItemDto_CarouselId",
                table: "InstaImages");

            migrationBuilder.DropForeignKey(
                name: "FK_InstaImages_InstaPosts_PostId",
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

            migrationBuilder.DropForeignKey(
                name: "FK_InstaVideos_InstaPosts_PostId",
                table: "InstaVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstaVideos",
                table: "InstaVideos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstaImages",
                table: "InstaImages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstagramLocationDto",
                table: "InstagramLocationDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstagramCarouselItemDto",
                table: "InstagramCarouselItemDto");

            migrationBuilder.DropPrimaryKey(
                name: "PK_InstagramCaptionDto",
                table: "InstagramCaptionDto");

            migrationBuilder.RenameTable(
                name: "InstaVideos",
                newName: "Videos");

            migrationBuilder.RenameTable(
                name: "InstaImages",
                newName: "Images");

            migrationBuilder.RenameTable(
                name: "InstagramLocationDto",
                newName: "Locations");

            migrationBuilder.RenameTable(
                name: "InstagramCarouselItemDto",
                newName: "CarouselItems");

            migrationBuilder.RenameTable(
                name: "InstagramCaptionDto",
                newName: "Captions");

            migrationBuilder.RenameIndex(
                name: "IX_InstaVideos_PostId",
                table: "Videos",
                newName: "IX_Videos_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_InstaVideos_CarouselId",
                table: "Videos",
                newName: "IX_Videos_CarouselId");

            migrationBuilder.RenameIndex(
                name: "IX_InstaImages_PostId",
                table: "Images",
                newName: "IX_Images_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_InstaImages_CarouselId",
                table: "Images",
                newName: "IX_Images_CarouselId");

            migrationBuilder.RenameIndex(
                name: "IX_InstagramCarouselItemDto_PostId",
                table: "CarouselItems",
                newName: "IX_CarouselItems_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Videos",
                table: "Videos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locations",
                table: "Locations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarouselItems",
                table: "CarouselItems",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Captions",
                table: "Captions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarouselItems_InstaPosts_PostId",
                table: "CarouselItems",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_CarouselItems_CarouselId",
                table: "Images",
                column: "CarouselId",
                principalTable: "CarouselItems",
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
                name: "FK_Videos_CarouselItems_CarouselId",
                table: "Videos",
                column: "CarouselId",
                principalTable: "CarouselItems",
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarouselItems_InstaPosts_PostId",
                table: "CarouselItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_CarouselItems_CarouselId",
                table: "Images");

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
                name: "FK_Videos_CarouselItems_CarouselId",
                table: "Videos");

            migrationBuilder.DropForeignKey(
                name: "FK_Videos_InstaPosts_PostId",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Videos",
                table: "Videos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locations",
                table: "Locations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarouselItems",
                table: "CarouselItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Captions",
                table: "Captions");

            migrationBuilder.RenameTable(
                name: "Videos",
                newName: "InstaVideos");

            migrationBuilder.RenameTable(
                name: "Locations",
                newName: "InstagramLocationDto");

            migrationBuilder.RenameTable(
                name: "Images",
                newName: "InstaImages");

            migrationBuilder.RenameTable(
                name: "CarouselItems",
                newName: "InstagramCarouselItemDto");

            migrationBuilder.RenameTable(
                name: "Captions",
                newName: "InstagramCaptionDto");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_PostId",
                table: "InstaVideos",
                newName: "IX_InstaVideos_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Videos_CarouselId",
                table: "InstaVideos",
                newName: "IX_InstaVideos_CarouselId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_PostId",
                table: "InstaImages",
                newName: "IX_InstaImages_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Images_CarouselId",
                table: "InstaImages",
                newName: "IX_InstaImages_CarouselId");

            migrationBuilder.RenameIndex(
                name: "IX_CarouselItems_PostId",
                table: "InstagramCarouselItemDto",
                newName: "IX_InstagramCarouselItemDto_PostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstaVideos",
                table: "InstaVideos",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstagramLocationDto",
                table: "InstagramLocationDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstaImages",
                table: "InstaImages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstagramCarouselItemDto",
                table: "InstagramCarouselItemDto",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_InstagramCaptionDto",
                table: "InstagramCaptionDto",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_InstagramCarouselItemDto_InstaPosts_PostId",
                table: "InstagramCarouselItemDto",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaImages_InstagramCarouselItemDto_CarouselId",
                table: "InstaImages",
                column: "CarouselId",
                principalTable: "InstagramCarouselItemDto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InstaImages_InstaPosts_PostId",
                table: "InstaImages",
                column: "PostId",
                principalTable: "InstaPosts",
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

            migrationBuilder.AddForeignKey(
                name: "FK_InstaVideos_InstaPosts_PostId",
                table: "InstaVideos",
                column: "PostId",
                principalTable: "InstaPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
