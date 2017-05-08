using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Classes.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Console.Providers.PostgreSQL
{
    public class InstaPostsRepository : IDataAccessProvider<InstaMediaDb>
    {
        private readonly InstaCrafterPgsqlContext _context;
        private readonly ILogger _logger;

        public InstaPostsRepository(InstaCrafterPgsqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaPostsRepository");
        }

        public void Add(InstaMediaDb item)
        {
            if (Exist(item)) return;
            _context.InstaPosts.Add(item);
            _context.SaveChanges();
        }

        public void Update(int userId, InstaMediaDb item)
        {
            if (!Exist(item)) return;
            _context.InstaPosts.Update(item);
            _context.SaveChanges();
        }

        public void Delete(int userId)
        {
            var entity = _context.InstaPosts.First(t => t.Id == userId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstaMediaDb Get(int id)
        {
            if (!_context.InstaPosts.Any(t => t.Id == id)) return InstaMediaDb.Empty;
            return _context.InstaPosts.First(t => t.Id == id);
        }

        public InstaMediaDb Get(string code)
        {
            if (!_context.InstaPosts.Any(t => t.Code == code)) return InstaMediaDb.Empty;
            return _context.InstaPosts.First(t => t.Code == code);
        }

        public List<InstaMediaDb> GetItems()
        {
            return
                _context.InstaPosts.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaMediaDb item)
        {
            return _context.InstaPosts.Any(post => post.Pk == item.Pk);
        }
    }
}