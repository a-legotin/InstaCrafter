using System;
using InstaCrafter.DataStore.Providers.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;

namespace InstaCrafter.DataStore.Migrations
{
    [DbContext(typeof(PostgreSqlDatabaseContext))]
    internal class PostgreSqlDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752");

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaMediaDb", b =>
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

            modelBuilder.Entity("InstaCrafter.Classes.Database.InstaMediaDb", b =>
            {
                b.HasOne("InstaCrafter.Classes.Database.InstaUserDb", "User")
                    .WithMany("PostCollection")
                    .HasForeignKey("UserId");
            });
        }
    }
}