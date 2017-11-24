using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using InstaBackup.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaBackup.DataAccess
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

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaStory>().HasKey(m => m.InternalStoryId);
            builder.Entity<InstaMediaPost>().HasKey(m => m.InternalPostId);
            builder.Entity<InstaUser>().HasKey(m => m.InternalUserId);

            builder.Entity<InstaStory>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaMediaPost>().Property<DateTime>("UpdatedTimestamp");
            builder.Entity<InstaUser>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            UpdateUpdatedProperty<InstaStory>();
            UpdateUpdatedProperty<InstaMediaPost>();
            UpdateUpdatedProperty<InstaUser>();

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