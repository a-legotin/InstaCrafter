using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Providers.PostgreSQL
{
    public class InstaUsersRepository : IDataAccessProvider<InstaUser>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstaUsersRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaUsersRepository");
        }

        public void Add(InstaUser item)
        {
            _context.InstaUsers.Add(item);
            _context.SaveChanges();
        }

        public void Update(int postId, InstaUser item)
        {
            _context.InstaUsers.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int postId)
        {
            var entity = _context.InstaUsers.First(t => t.Id == postId);
            _context.InstaUsers.Remove(entity);
            _context.SaveChanges();
        }
        public InstaUser Get(string name)
        {
            if (!_context.InstaUsers.Any(t => t.UserName == name)) return InstaUser.Empty;
            return _context.InstaUsers.First(t => t.UserName == name);
        }
        public InstaUser Get(int postId)
        {
            return _context.InstaUsers.First(t => t.Id == postId);
        }

        public List<InstaUser> GetItems()
        {
            return _context.InstaUsers.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaUser item)
        {
            return _context.InstaUsers.Any(user => user.InstaIdentifier == item.InstaIdentifier);
        }
    }
}
