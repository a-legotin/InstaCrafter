using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using InstaCrafter;

namespace InstaCrafter.Migrations
{
    [DbContext(typeof(PostgreSQLDatabaseContext))]
    partial class PostgreSQLDatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("InstaCrafter.Models.InstaPost", b =>
                {
                    b.Property<long>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("UpdatedTimestamp");

                    b.Property<string>("Url");

                    b.HasKey("PostId");

                    b.ToTable("InstaPosts");
                });
        }
    }
}
