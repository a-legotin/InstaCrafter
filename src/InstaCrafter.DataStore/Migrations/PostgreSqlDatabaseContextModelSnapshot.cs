using System;
using InstaCrafter.Providers.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InstaCrafter.Migrations
{
    [DbContext(typeof(PostgreSqlDatabaseContext))]
    internal class PostgreSqlDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaPost", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd();

                b.Property<bool>("CanViewComment");

                b.Property<string>("Code");

                b.Property<DateTime>("CreatedTime");

                b.Property<string>("Link");

                b.Property<DateTime>("UpdatedTimestamp");

                b.Property<long>("UserId");

                b.HasKey("Id");

                b.ToTable("InstaPosts");
            });

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
        }
    }
}