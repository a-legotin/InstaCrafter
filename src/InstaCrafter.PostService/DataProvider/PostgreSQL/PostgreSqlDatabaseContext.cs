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

        public DbSet<InstagramPostDto> Posts { get; set; }
        public DbSet<InstagramVideoDto> Videos { get; set; }
        public DbSet<InstagramImageDto> Images { get; set; }
        public DbSet<InstagramCarouselItemDto> CarouselItems { get; set; }
        public DbSet<InstagramCaptionDto> Captions { get; set; }
        public DbSet<InstagramLocationDto> Locations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstagramPostDto>().HasKey(m => m.Id);
            builder.Entity<InstagramPostDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramImageDto>().HasKey(m => m.Id);
            builder.Entity<InstagramImageDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramVideoDto>().HasKey(m => m.Id);
            builder.Entity<InstagramVideoDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramLocationDto>().HasKey(m => m.Id);
            builder.Entity<InstagramLocationDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramCarouselItemDto>().HasKey(m => m.Id);
            builder.Entity<InstagramCarouselItemDto>().Property<DateTime>("UpdatedTimestamp");
            
            builder.Entity<InstagramCaptionDto>().HasKey(m => m.Id);
            builder.Entity<InstagramCaptionDto>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<InstagramPostDto>()
                .HasMany(post => post.Images)
                .WithOne(image => image.Post);

            builder.Entity<InstagramPostDto>()
                .HasMany(post => post.Videos)
                .WithOne(video => video.Post);
            
            builder.Entity<InstagramPostDto>()
                .HasMany(post => post.Carousel)
                .WithOne(carousel => carousel.Post);

            builder.Entity<InstagramPostDto>()
                .HasOne(post => post.Caption);

            builder.Entity<InstagramCarouselItemDto>()
                .HasMany(carousel => carousel.Images)
                .WithOne(image => image.Carousel);
            
            builder.Entity<InstagramCarouselItemDto>()
                .HasMany(carousel => carousel.Videos)
                .WithOne(image => image.Carousel);
            
            builder.Entity<InstagramLocationDto>()
                .HasMany(location => location.Posts)
                .WithOne(post => post.Location);
            
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstagramPostDto>();
            updateUpdatedProperty<InstagramImageDto>();
            updateUpdatedProperty<InstagramVideoDto>();
            updateUpdatedProperty<InstagramLocationDto>();
            updateUpdatedProperty<InstagramCaptionDto>();
            updateUpdatedProperty<InstagramCarouselItemDto>();

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