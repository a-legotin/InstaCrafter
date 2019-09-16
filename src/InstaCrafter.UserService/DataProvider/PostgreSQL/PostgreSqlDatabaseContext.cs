using System;
using System.Linq;
using InstaCrafter.Classes.Models;
using InstaCrafter.UserService.DtoModels;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.UserService.DataProvider.PostgreSQL
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext()
        {
        }

        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<InstagramUserDto> InstaUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstagramUserDto>().HasKey(m => m.Id);
            builder.Entity<InstagramUserDto>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstagramUserDto>();
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