using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter
{
    public class InstaPost
    {
        public long PostId { get; set; }
        public string Url { get; set; }
    }

    public class DomainModelPostgreSqlContext : DbContext
    {
        public DomainModelPostgreSqlContext()
        {

        }
        public DomainModelPostgreSqlContext(DbContextOptions<DomainModelPostgreSqlContext> options) : base(options)
        {
        }

        public DbSet<InstaPost> InstaPosts { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<InstaPost>().HasKey(m => m.PostId);
            builder.Entity<InstaPost>().Property<DateTime>("UpdatedTimestamp");
            base.OnModelCreating(builder);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();
            updateUpdatedProperty<InstaPost>();
            return base.SaveChanges();
        }

        private void updateUpdatedProperty<T>() where T : class
        {
            var modifiedSourceInfo =
                ChangeTracker.Entries<T>()
                    .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in modifiedSourceInfo)
            {
                entry.Property("UpdatedTimestamp").CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
