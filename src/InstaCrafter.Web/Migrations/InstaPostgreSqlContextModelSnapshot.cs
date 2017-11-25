﻿// <auto-generated />
using InstaCrafter.Web.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace InstaCrafter.Web.Migrations
{
    [DbContext(typeof(InstaPostgreSqlContext))]
    partial class InstaPostgreSqlContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaCaption", b =>
                {
                    b.Property<long>("InternalCaptionId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("CreatedAtUtc");

                    b.Property<string>("MediaId");

                    b.Property<string>("Pk");

                    b.Property<string>("Text");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<long?>("UserInternalUserId");

                    b.HasKey("InternalCaptionId");

                    b.HasIndex("UserInternalUserId");

                    b.ToTable("InstaCaptions");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaCarouselItem", b =>
                {
                    b.Property<long>("InternalCarouselItemId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("CarouselParentId");

                    b.Property<int>("Height");

                    b.Property<string>("InstaIdentifier");

                    b.Property<long?>("InstaMediaPostInternalPostId");

                    b.Property<string>("Pk");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<int>("Width");

                    b.HasKey("InternalCarouselItemId");

                    b.HasIndex("InstaMediaPostInternalPostId");

                    b.ToTable("InstaCarouselItems");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaMediaInfo", b =>
                {
                    b.Property<long>("InternalMediaId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Height");

                    b.Property<long?>("InstaCarouselItemInternalCarouselItemId");

                    b.Property<long?>("InstaMediaPostInternalPostId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("Url");

                    b.Property<int>("Width");

                    b.HasKey("InternalMediaId");

                    b.HasIndex("InstaCarouselItemInternalCarouselItemId");

                    b.HasIndex("InstaMediaPostInternalPostId");

                    b.ToTable("InstaMedias");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaMediaPost", b =>
                {
                    b.Property<long>("InternalPostId")
                        .ValueGeneratedOnAdd();

                    b.Property<long?>("CaptionInternalCaptionId");

                    b.Property<string>("ClientCacheKey");

                    b.Property<string>("Code");

                    b.Property<string>("CommentsCount");

                    b.Property<DateTime?>("DeviceTimeStap");

                    b.Property<string>("FilterType");

                    b.Property<bool>("HasAudio");

                    b.Property<bool>("HasLiked");

                    b.Property<string>("InstaIdentifier");

                    b.Property<long?>("InstaStoryInternalStoryId");

                    b.Property<bool>("IsMultiPost");

                    b.Property<int>("LikesCount");

                    b.Property<int>("MediaType");

                    b.Property<bool>("PhotoOfYou");

                    b.Property<string>("Pk");

                    b.Property<DateTime?>("TakenAt");

                    b.Property<string>("TrakingToken");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<long?>("UserInternalUserId");

                    b.Property<int>("ViewCount");

                    b.HasKey("InternalPostId");

                    b.HasIndex("CaptionInternalCaptionId");

                    b.HasIndex("InstaStoryInternalStoryId");

                    b.HasIndex("UserInternalUserId");

                    b.ToTable("InstaPosts");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaStory", b =>
                {
                    b.Property<long>("InternalStoryId")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanReply");

                    b.Property<DateTime?>("ExpiringAt");

                    b.Property<string>("Id");

                    b.Property<string>("LatestReelMedia");

                    b.Property<bool>("Muted");

                    b.Property<int>("PrefetchCount");

                    b.Property<int>("RankedPosition");

                    b.Property<DateTime?>("Seen");

                    b.Property<int>("SeenRankedPosition");

                    b.Property<string>("SocialContext");

                    b.Property<string>("SourceToken");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<long?>("UserInternalUserId");

                    b.HasKey("InternalStoryId");

                    b.HasIndex("UserInternalUserId");

                    b.ToTable("InstaStories");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaUser", b =>
                {
                    b.Property<long>("InternalUserId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName");

                    b.Property<bool>("IsPrivate");

                    b.Property<bool>("IsVerified");

                    b.Property<long>("Pk");

                    b.Property<string>("ProfilePicture");

                    b.Property<string>("ProfilePictureId");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("UserName");

                    b.HasKey("InternalUserId");

                    b.ToTable("InstaUsers");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaCaption", b =>
                {
                    b.HasOne("InstaCrafter.Web.Models.InstaUser", "User")
                        .WithMany()
                        .HasForeignKey("UserInternalUserId");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaCarouselItem", b =>
                {
                    b.HasOne("InstaCrafter.Web.Models.InstaMediaPost")
                        .WithMany("Carousel")
                        .HasForeignKey("InstaMediaPostInternalPostId");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaMediaInfo", b =>
                {
                    b.HasOne("InstaCrafter.Web.Models.InstaCarouselItem")
                        .WithMany("Medias")
                        .HasForeignKey("InstaCarouselItemInternalCarouselItemId");

                    b.HasOne("InstaCrafter.Web.Models.InstaMediaPost")
                        .WithMany("Images")
                        .HasForeignKey("InstaMediaPostInternalPostId");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaMediaPost", b =>
                {
                    b.HasOne("InstaCrafter.Web.Models.InstaCaption", "Caption")
                        .WithMany()
                        .HasForeignKey("CaptionInternalCaptionId");

                    b.HasOne("InstaCrafter.Web.Models.InstaStory")
                        .WithMany("Items")
                        .HasForeignKey("InstaStoryInternalStoryId");

                    b.HasOne("InstaCrafter.Web.Models.InstaUser", "User")
                        .WithMany("Medias")
                        .HasForeignKey("UserInternalUserId");
                });

            modelBuilder.Entity("InstaCrafter.Web.Models.InstaStory", b =>
                {
                    b.HasOne("InstaCrafter.Web.Models.InstaUser", "User")
                        .WithMany("Stories")
                        .HasForeignKey("UserInternalUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
