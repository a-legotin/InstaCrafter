using System;
using System.Collections.Generic;
using System.Linq;
using InstaCrafter.CrafterJobs.DtoModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.CrafterJobs.DataProvider.PostgreSQL
{
    public class CrafterJobsRepository : IDataAccessProvider<InstaCrafterJobDto>
    {
        private readonly PostgreSqlDatabaseContext _context;
        private readonly ILogger _logger;

        public CrafterJobsRepository(PostgreSqlDatabaseContext context, ILoggerFactory loggerFactory)
        {
            _context = context;
            _logger = loggerFactory.CreateLogger("CrafterJobsRepository");
        }

        public void Add(InstaCrafterJobDto item)
        {
            _context.Jobs.Add(item);
            _context.SaveChanges();
        }

        public void Update(long id, InstaCrafterJobDto item)
        {
            _context.Jobs.Update(item);
            _context.SaveChanges();
        }

        public void Delete(long id)
        {
            var entity = _context.Jobs.First(t => t.Id == id);
            _context.Jobs.Remove(entity);
            _context.SaveChanges();
        }

        public InstaCrafterJobDto Get(int id)
        {
            if (!_context.Jobs.Any(t => t.Id == id)) return new InstaCrafterJobDto();
            return _context.Jobs.First(t => t.Id == id);
        }

        public InstaCrafterJobDto Get(long postId)
        {
            return _context.Jobs.First(t => t.Id == postId);
        }

        public List<InstaCrafterJobDto> GetItems()
        {
            return _context.Jobs.OrderByDescending(item => EF.Property<DateTime>(item, "UpdatedTimestamp")).ToList();
        }

        public bool Exist(InstaCrafterJobDto item)
        {
            return _context.Jobs.Any(postDto => postDto.Id == item.Id);
        }
    }
}