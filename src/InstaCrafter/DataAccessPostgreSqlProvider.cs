﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter
{
    public class DataAccessPostgreSqlProvider : IDataAccessProvider
    {
        private readonly DomainModelPostgreSqlContext _context;
        private readonly ILogger _logger;

        public DataAccessPostgreSqlProvider(DomainModelPostgreSqlContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("DataAccessPostgreSqlProvider");
        }

        public void AddPost(InstaPost dataEventRecord)
        {
            _context.InstaPosts.Add(dataEventRecord);
            _context.SaveChanges();
        }

        public void UpdatePost(long dataEventRecordId, InstaPost dataEventRecord)
        {
            _context.InstaPosts.Update(dataEventRecord);
            _context.SaveChanges();
        }

        public void DeletePost(long dataEventRecordId)
        {
            var entity = _context.InstaPosts.First(t => t.PostId == dataEventRecordId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstaPost GetPost(long dataEventRecordId)
        {
            return _context.InstaPosts.First(t => t.PostId == dataEventRecordId);
        }

        public List<InstaPost> GetPosts()
        {
            // Using the shadow property EF.Property<DateTime>(dataEventRecord)
            return _context.InstaPosts.OrderByDescending(dataEventRecord => EF.Property<DateTime>(dataEventRecord, "UpdatedTimestamp")).ToList();
        }
    }
}
