using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.PostService.DtoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.PostService.DataProvider.PostgreSQL
{
    public class InstagramPostsRepository : IDataAccessProvider<InstagramPostDto>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstagramPostsRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstagramPostsRepository");
        }

        public void Add(InstagramPostDto item)
        {
            _context.InstaPosts.Add(item);
            _context.SaveChanges();
        }

        public void Update(long postId, InstagramPostDto item)
        {
            _context.InstaPosts.Update(item);
            _context.SaveChanges();
        }

        public void Delete(long postId)
        {
            var entity = _context.InstaPosts.First(t => t.Id == postId);
            _context.InstaPosts.Remove(entity);
            _context.SaveChanges();
        }

        public InstagramPostDto Get(string code)
        {
            if (!_context.InstaPosts.Any(t => t.Code == code)) return new InstagramPostDto();
            return _context.InstaPosts.First(t => t.Code == code);
        }

        public InstagramPostDto Get(long postId)
        {
            return _context.InstaPosts.First(t => t.Id == postId);
        }

        public List<InstagramPostDto> GetItems()
        {
            return _context.InstaPosts.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstagramPostDto item)
        {
            return _context.InstaPosts.Any(postDto => postDto.Code == item.Code);
        }
    }
}