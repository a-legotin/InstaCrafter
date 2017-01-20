using System;
using System.Linq;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.Providers.PostgreSQL
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext()
        {
        }

        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<InstaPost> InstaPosts { get; set; }
        public DbSet<InstaUser> InstaUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaPost>().HasKey(m => m.Id);
            builder.Entity<InstaPost>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<InstaUser>().HasKey(m => m.Id);
            builder.Entity<InstaUser>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstaPost>();
            updateUpdatedProperty<InstaUser>();
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