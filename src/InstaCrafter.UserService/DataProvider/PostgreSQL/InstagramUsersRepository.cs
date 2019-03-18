using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Classes.Models;
using InstaCrafter.UserService.DtoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.UserService.DataProvider.PostgreSQL
{
    public class InstagramUsersRepository : IDataAccessProvider<InstagramUserDto>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public InstagramUsersRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("InstaUsersRepository");
        }

        public void Add(InstagramUserDto item)
        {
            _context.InstaUsers.Add(item);
            _context.SaveChanges();
        }

        public void Update(long userId, InstagramUserDto item)
        {
            _context.InstaUsers.Update(item);
            _context.SaveChanges();
        }

        public void Delete(long userId)
        {
            var entity = _context.InstaUsers.First(t => t.Id == userId);
            _context.InstaUsers.Remove(entity);
            _context.SaveChanges();
        }

        public InstagramUserDto Get(string userName)
        {
            if (!_context.InstaUsers.Any(t => t.UserName == userName)) return new InstagramUserDto();
            return _context.InstaUsers.First(t => t.UserName == userName);
        }

        public InstagramUserDto Get(long userId)
        {
            return _context.InstaUsers.First(t => t.Id == userId);
        }

        public List<InstagramUserDto> GetItems()
        {
            return _context.InstaUsers.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstagramUserDto item)
        {
            return _context.InstaUsers.Any(user => user.Pk == item.Pk);
        }
    }
}