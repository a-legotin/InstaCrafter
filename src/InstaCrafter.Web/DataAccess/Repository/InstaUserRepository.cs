using System.Collections.Generic;
using System.Linq;
using InstaBackup.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaBackup.DataAccess.Repository
{
    public class InstaUserRepository : IRepository<InstaUser>
    {
        private readonly InstaPostgreSqlContext _context;

        public InstaUserRepository(InstaPostgreSqlContext context)
        {
            _context = context;
        }

        public IEnumerable<InstaUser> GetAll()
        {
            return _context.InstaUsers.ToList();
        }

        public InstaUser GetByInstaId(string id)
        {
            return _context.InstaUsers.SingleOrDefault(user => user.Pk == long.Parse(id));
        }

        public InstaUser GetById(long id)
        {
            return _context.InstaUsers.Find(id);
        }

        public void Create(InstaUser item)
        {
            _context.InstaUsers.Add(item);
        }

        public void Update(InstaUser item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var user = _context.InstaUsers.Find(id);
            _context.InstaUsers.Remove(user);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void CreateOrUpdate(InstaUser item)
        {
            var existing = GetByInstaId(item.Pk.ToString());
            if (existing != null)
            {
                item.InternalUserId = existing.InternalUserId;
                Update(existing);
            }
            else
                Create(item);
        }

        public bool ExistByInstaId(string id)
        {
            return _context.InstaUsers.Any(user => user.Pk == long.Parse(id));
        }
    }
}