using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter
{
    public class DataAccessPostgreSqlProvider : IDataAccessProvider
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public DataAccessPostgreSqlProvider(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessPostgreSqlProvider");
        }

        public void AddPost(InstaPost dataEventRecord)
        {
            _context.InstaPosts.Add(dataEventRecord);
            _context.SaveChanges();
        }

        public void UpdatePost(int postId, InstaPost dataEventRecord)
        {
            _context.InstaPosts.Update(dataEventRecord);
            _context.SaveChanges();
        }

        public void DeletePost(int postId)
        {
            var entity = _context.InstaPosts.First(t => t.PostId == postId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstaPost GetPost(int postId)
        {
            return _context.InstaPosts.First(t => t.PostId == postId);
        }

        public List<InstaPost> GetPosts()
        {
            return _context.InstaPosts.OrderByDescending(dataEventRecord => EF.Property<DateTime>(dataEventRecord, "UpdatedTimestamp")).ToList();
        }
    }
}
