using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InstaCrafter;

namespace InstaCrafter.DataStore.Migrations
{
    [DbContext(typeof(PostgreSqlDatabaseContext))]
    partial class PostgreSqlDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FullName");

                    b.Property<long>("InstaIdentifier");

                    b.Property<string>("ProfilePicture");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("InstaUsers");
                });

            modelBuilder.Entity("InstaCrafter.Models.InstaPost", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("CanViewComment");

                    b.Property<string>("Code");

                    b.Property<DateTime>("CreatedTime");

                    b.Property<string>("Link");

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("Url");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("InstaPosts");
                });
        }
    }
}
