using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Core.Entities;
using InstaCrafter.Core.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.Identity.Infrastructure.Data.Repositories
{

    public abstract class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly DbContext DbContext;

        protected EfRepository(DbContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual async Task<T> GetById(int id)
        {
            return await DbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> ListAll()
        {
            return await DbContext.Set<T>().ToListAsync();
        }

        public async Task<T?> GetSingleBySpec(ISpecification<T> spec)
        {
            var result = await List(spec);
            return result.FirstOrDefault();
        }

        public async Task<List<T>> List(ISpecification<T> spec)
        {
            // fetch a Queryable that includes all expression-based includes
            var queryableResultWithIncludes = spec.Includes
                .Aggregate(DbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include));

            // modify the IQueryable to include any string-based include statements
            var secondaryResult = spec.IncludeStrings
                .Aggregate(queryableResultWithIncludes,
                    (current, include) => current.Include(include));

            // return the result of the query using the specification's criteria expression
            return await secondaryResult
                            .Where(spec.Criteria)
                            .ToListAsync();
        }


        public async Task<T> Add(T entity)
        {
            DbContext.Set<T>().Add(entity);
            await DbContext.SaveChangesAsync();
            return entity;
        }

        public async Task Update(T entity)
        {
            DbContext.Entry(entity).State = EntityState.Modified;
            await DbContext.SaveChangesAsync();
        }

        public async Task Delete(T entity)
        {
            DbContext.Set<T>().Remove(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
