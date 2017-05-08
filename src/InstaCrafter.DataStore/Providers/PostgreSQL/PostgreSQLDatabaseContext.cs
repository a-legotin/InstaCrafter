﻿using System;
using System.Linq;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.DataStore.Providers.PostgreSQL
{
    public class PostgreSqlDatabaseContext : DbContext
    {
        public PostgreSqlDatabaseContext(DbContextOptions<PostgreSqlDatabaseContext> options) : base(options)
        {
        }

        public DbSet<InstaMediaDb> InstaPosts { get; set; }
        public DbSet<InstaUserDb> InstaUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaMediaDb>().HasKey(m => m.Id);
            builder.Entity<InstaMediaDb>().Property<DateTime>("UpdatedTimestamp");

            builder.Entity<InstaUserDb>().HasKey(m => m.Id);
            builder.Entity<InstaUserDb>().Property<DateTime>("UpdatedTimestamp");

            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstaMediaDb>();
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