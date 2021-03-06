﻿using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.DataAccess.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Providers.PostgreSQL
{
    public class InstaPostsRepository : IDataAccessProvider<InstaPost>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstaPostsRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaPostsRepository");
        }

        public void Add(InstaPost item)
        {
            _context.InstaPosts.Add(item);
            _context.SaveChanges();
        }

        public void Update(int postId, InstaPost item)
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

        public InstaPost Get(int id)
        {
            if (!_context.InstaPosts.Any(t => t.Id == id)) return InstaPost.Empty;
            return _context.InstaPosts.First(t => t.Id == id);
        }

        public InstaPost Get(string code)
        {
            if (!_context.InstaPosts.Any(t => t.Code == code)) return InstaPost.Empty;
            return _context.InstaPosts.First(t => t.Code == code);
        }

        public List<InstaPost> GetItems()
        {
            return
                _context.InstaPosts.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaPost item)
        {
            return _context.InstaPosts.Any(post => post.Code == item.Code);
        }
    }
}