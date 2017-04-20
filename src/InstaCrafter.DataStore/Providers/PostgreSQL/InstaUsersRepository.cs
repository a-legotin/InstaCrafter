using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Providers.PostgreSQL
{
    public class InstaUsersRepository : IDataAccessProvider<InstaUserDb>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstaUsersRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaUsersRepository");
        }

        public void Add(InstaUserDb item)
        {
            _context.InstaUsers.Add(item);
            _context.SaveChanges();
        }

        public void Update(int postId, InstaUserDb item)
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

        public InstaUserDb Get(string name)
        {
            if (!_context.InstaUsers.Any(t => t.UserName == name)) return InstaUserDb.Empty;
            return _context.InstaUsers.First(t => t.UserName == name);
        }

        public InstaUserDb Get(int postId)
        {
            return _context.InstaUsers.First(t => t.Id == postId);
        }

        public List<InstaUserDb> GetItems()
        {
            return
                _context.InstaUsers.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaUserDb item)
        {
            return _context.InstaUsers.Any(user => user.InstaIdentifier == item.InstaIdentifier);
        }
    }
}