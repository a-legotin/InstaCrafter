using System;
using System.Linq;
using InstaCrafter.Classes.Database;
using InstagramApi.Classes;
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

        public DbSet<InstaPostDb> InstaPosts { get; set; }
        public DbSet<InstaUserDb> InstaUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaPostDb>().HasKey(m => m.Id);
            builder.Entity<InstaPostDb>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<InstaUserDb>().HasKey(m => m.Id);
            builder.Entity<InstaUserDb>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstaPostDb>();
            updateUpdatedProperty<InstaUserDb>();
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