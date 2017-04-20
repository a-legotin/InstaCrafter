using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InstaCrafter.Providers.PostgreSQL;

namespace InstaCrafter.DataStore.Migrations
{
    [DbContext(typeof(PostgreSqlDatabaseContext))]
    [Migration("20170420130116_NewGeneration_1")]
    partial class NewGeneration_1
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaPostDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("ImageLink");

                    b.Property<string>("Link");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("InstaPosts");
                });

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaUserDb", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ExternalUrl");

                    b.Property<int>("FollowedByCount");

                    b.Property<string>("FullName");

                    b.Property<long>("InstaIdentifier");

                    b.Property<string>("IsVerified");

                    b.Property<string>("ProfilePicture");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("InstaUsers");
                });

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaPostDb", b =>
                {
                    b.HasOne("InstaCrafter.Classes.Database.InstaUserDb", "User")
                        .WithMany("PostCollection")
                        .HasForeignKey("UserId");
                });
        }
    }
}
