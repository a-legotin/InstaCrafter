using System;
using System.Linq;
using InstaCrafter.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.Web.DataAccess
{
    public class InstaPostgreSqlContext : DbContext
    {
        public InstaPostgreSqlContext()
        {
            
        }

        public InstaPostgreSqlContext(DbContextOptions<InstaPostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<InstaStory> InstaStories { get; set; }
        public DbSet<InstaMediaPost> InstaPosts { get; set; }
        public DbSet<InstaUser> InstaUsers { get; set; }
        public DbSet<InstaCaption> InstaCaptions { get; set; }
        public DbSet<InstaCarouselItem> InstaCarouselItems { get; set; }
        public DbSet<InstaMediaInfo> InstaMedias { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaStory>().HasKey(m => m.InternalStoryId);
            builder.Entity<InstaMediaPost>().HasKey(m => m.InternalPostId);
            builder.Entity<InstaUser>().HasKey(m => m.InternalUserId);
            builder.Entity<InstaCaption>().HasKey(m => m.InternalCaptionId);
            builder.Entity<InstaCarouselItem>().HasKey(m => m.InternalCarouselItemId);
            builder.Entity<InstaMediaInfo>().HasKey(m => m.InternalMediaId);

            builder.Entity<InstaStory>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaMediaPost>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaUser>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaCaption>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaCarouselItem>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaMediaInfo>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            UpdateUpdatedProperty<InstaStory>();
            UpdateUpdatedProperty<InstaMediaPost>();
            UpdateUpdatedProperty<InstaUser>();
            UpdateUpdatedProperty<InstaCaption>();
            UpdateUpdatedProperty<InstaCarouselItem>();
            UpdateUpdatedProperty<InstaMediaInfo>();

            return base.SaveChanges();
        }

        private void UpdateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
        }
    }
}