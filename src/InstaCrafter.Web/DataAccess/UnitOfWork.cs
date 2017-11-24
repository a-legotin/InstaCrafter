using System;
using System.Linq;
using InstaBackup.DataAccess.Repository;
using InstaBackup.Models;
using Microsoft.EntityFrameworkCore;

namespace InstaBackup.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InstaPostgreSqlContext _dbContext;

        private readonly Lazy<IRepository<InstaMediaPost>> _postRepository;

        private readonly Lazy<IRepository<InstaStory>> _storyRepository;

        private readonly Lazy<IRepository<InstaUser>> _userRepository;

        private bool _disposed;

        public UnitOfWork(InstaPostgreSqlContext dbContext)
        {
            _dbContext = dbContext;
            _userRepository = new Lazy<IRepository<InstaUser>>(() => new InstaUserRepository(_dbContext));
            _storyRepository = new Lazy<IRepository<InstaStory>>(() => new InstaStoryRepository(_dbContext));
            _postRepository = new Lazy<IRepository<InstaMediaPost>>(() => new InstaMediaRepository(_dbContext));
        }


        public IRepository<InstaStory> StoryRepository => _storyRepository.Value;
        public IRepository<InstaMediaPost> PostRepository => _postRepository.Value;
        public IRepository<InstaUser> UserRepository => _userRepository.Value;


        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public void RejectChanges()
        {
            foreach (var entry in _dbContext.ChangeTracker.Entries()
                .Where(e => e.State != EntityState.Unchanged))
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.State = EntityState.Detached;
                        break;
                    case EntityState.Modified:
                    case EntityState.Deleted:
                        entry.Reload();
                        break;
                }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _dbContext.Dispose();
                _disposed = true;
            }
        }
    }
}