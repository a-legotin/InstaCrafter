using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.Web.DataAccess.Repository
{
    public class InstaMediaRepository : IRepository<InstaMediaPost>
    {
        private readonly InstaPostgreSqlContext _context;

        public InstaMediaRepository(InstaPostgreSqlContext context)
        {
            _context = context;
        }
        public IEnumerable<InstaMediaPost> GetAll()
        {
           return _context.InstaPosts.ToList();
        }

        public InstaMediaPost GetByInstaId(string id)
        {
            return _context.InstaPosts.SingleOrDefault(post => post.Code == id);
        }

        public InstaMediaPost GetById(long id)
        {
            return _context.InstaPosts.Find(id);
        }

        public void Create(InstaMediaPost item)
        {
            _context.InstaPosts.Add(item);
        }

        public void Update(InstaMediaPost item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var post = _context.InstaPosts.Find(id);
            _context.InstaPosts.Remove(post);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void CreateOrUpdate(InstaMediaPost item)
        {
            var existing = GetByInstaId(item.Code);
            if (existing != null)
            {
                item.InternalPostId = existing.InternalPostId;
                Update(existing);
            }
            else
                Create(item);
        }

        public bool ExistByInstaId(string id)
        {
            return _context.InstaPosts.Any(post => post.Code == id);
        }
    }
}