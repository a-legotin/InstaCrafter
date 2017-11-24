﻿// <auto-generated />
using InstaBackup.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace InstaBackup.Migrations
{
    [DbContext(typeof(InstaPostgreSqlContext))]
    [Migration("20171112124257_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("InstaBackup.Models.InstaMediaPost", b =>
                {
                    b.Property<long>("InternalPostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Caption");

                    b.Property<string>("ClientCacheKey");

                    b.Property<string>("Code");

                    b.Property<string>("CommentsCount");

                    b.Property<DateTime?>("DeviceTimeStap");

                    b.Property<string>("FilterType");

                    b.Property<bool>("HasAudio");

                    b.Property<bool>("HasLiked");

                    b.Property<string>("Height");

                    b.Property<string>("ImageUrl");

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

                    b.Property<int>("Width");

                    b.HasKey("InternalPostId");

                    b.HasIndex("InstaStoryInternalStoryId");

                    b.HasIndex("UserInternalUserId");

                    b.ToTable("InstaPosts");
                });

            modelBuilder.Entity("InstaBackup.Models.InstaStory", b =>
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

            modelBuilder.Entity("InstaBackup.Models.InstaUser", b =>
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

            modelBuilder.Entity("InstaBackup.Models.InstaMediaPost", b =>
                {
                    b.HasOne("InstaBackup.Models.InstaStory")
                        .WithMany("Items")
                        .HasForeignKey("InstaStoryInternalStoryId");

                    b.HasOne("InstaBackup.Models.InstaUser", "User")
                        .WithMany("Medias")
                        .HasForeignKey("UserInternalUserId");
                });

            modelBuilder.Entity("InstaBackup.Models.InstaStory", b =>
                {
                    b.HasOne("InstaBackup.Models.InstaUser", "User")
                        .WithMany("Stories")
                        .HasForeignKey("UserInternalUserId");
                });
#pragma warning restore 612, 618
        }
    }
}
