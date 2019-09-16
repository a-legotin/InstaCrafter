using System;
using System.Linq;
using InstaCrafter.CrafterJobs.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.CrafterJobs.DataProvider.PostgreSQL
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext()
        {
        }

        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<InstaCrafterJobDto> Jobs { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaCrafterJobDto>().HasKey(m => m.Id);
            builder.Entity<InstaCrafterJobDto>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstaCrafterJobDto>();

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