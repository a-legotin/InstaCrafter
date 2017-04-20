using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Providers.PostgreSQL
{
    public class InstaPostsRepository : IDataAccessProvider<InstaPostDb>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstaPostsRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaPostsRepository");
        }

        public void Add(InstaPostDb item)
        {
            _context.InstaPosts.Add(item);
            _context.SaveChanges();
        }

        public void Update(int postId, InstaPostDb item)
        {
            _context.InstaPosts.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int postId)
        {
            var entity = _context.InstaPosts.First(t => t.Id == postId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstaPostDb Get(int id)
        {
            if (!_context.InstaPosts.Any(t => t.Id == id)) return InstaPostDb.Empty;
            return _context.InstaPosts.First(t => t.Id == id);
        }

        public InstaPostDb Get(string code)
        {
            if (!_context.InstaPosts.Any(t => t.Code == code)) return InstaPostDb.Empty;
            return _context.InstaPosts.First(t => t.Code == code);
        }

        public List<InstaPostDb> GetItems()
        {
            return
                _context.InstaPosts.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaPostDb item)
        {
            return _context.InstaPosts.Any(post => post.Code == item.Code);
        }
    }
}