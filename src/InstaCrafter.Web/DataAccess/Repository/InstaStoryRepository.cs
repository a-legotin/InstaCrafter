using System.Collections.Generic;
using System.Linq;
using InstaCrafter.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaCrafter.Web.DataAccess.Repository
{
    public class InstaStoryRepository : IRepository<InstaStory>
    {
        private readonly InstaPostgreSqlContext _context;

        public InstaStoryRepository(InstaPostgreSqlContext context)
        {
            _context = context;
        }

        public IEnumerable<InstaStory> GetAll()
        {
            return _context.InstaStories.ToList();
        }

        public InstaStory GetByInstaId(string id)
        {
            return _context.InstaStories.SingleOrDefault(story => story.Id == id);
        }

        public InstaStory GetById(long id)
        {
            return _context.InstaStories.Find(id);
        }

        public void Create(InstaStory item)
        {
            _context.InstaStories.Add(item);
        }

        public void Update(InstaStory item)
        {
            _context.Entry(item).State = EntityState.Modified;
        }

        public void Delete(long id)
        {
            var story = _context.InstaStories.Find(id);
            _context.InstaStories.Remove(story);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public void CreateOrUpdate(InstaStory item)
        {
            var existing = GetByInstaId(item.Id);
            if (existing != null)
            {
                item.Id = existing.Id;
                Update(existing);
            }
            else
                Create(item);
        }

        public bool ExistByInstaId(string id)
        {
            return _context.InstaStories.Any(user => user.Id == id);
        }
    }
}