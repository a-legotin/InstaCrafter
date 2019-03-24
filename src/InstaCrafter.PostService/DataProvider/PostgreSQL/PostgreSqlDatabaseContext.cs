using System;
using System.Linq;
using InstaCrafter.PostService.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.PostService.DataProvider.PostgreSQL
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext()
        {
        }

        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<InstagramPostDto> InstaPosts { get; set; }
        public DbSet<InstagramVideoDto> InstaVideos { get; set; }
        public DbSet<InstagramImageDto> InstaImages { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstagramPostDto>().HasKey(m => m.Id);
            builder.Entity<InstagramPostDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramImageDto>().HasKey(m => m.Id);
            builder.Entity<InstagramImageDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramVideoDto>().HasKey(m => m.Id);
            builder.Entity<InstagramVideoDto>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<InstagramPostDto>()
                .HasMany(post => post.Images)
                .WithOne(image => image.Post);

            builder.Entity<InstagramPostDto>()
                .HasMany(post => post.Videos)
                .WithOne(video => video.Post);
            
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstagramPostDto>();
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
        }
    }
}