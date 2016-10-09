using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter
{
    public class DataAccessPostgreSqlProvider : IDataAccessProvider<InstaPost>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public DataAccessPostgreSqlProvider(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessPostgreSqlProvider");
        }

        public void Add(InstaPost dataEventRecord)
        {
            _context.InstaPosts.Add(dataEventRecord);
            _context.SaveChanges();
        }

        public void Update(int postId, InstaPost dataEventRecord)
        {
            _context.InstaPosts.Update(dataEventRecord);
            _context.SaveChanges();
        }

        public void Delete(int postId)
        {
            var entity = _context.InstaPosts.First(t => t.Id == postId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstaPost Get(int postId)
        {
            return _context.InstaPosts.First(t => t.Id == postId);
        }

        public List<InstaPost> GetItems()
        {
            return _context.InstaPosts.OrderByDescending(dataEventRecord => EF.Property<DateTime>(dataEventRecord, "UpdatedTimestamp")).ToList();
        }
    }
}
