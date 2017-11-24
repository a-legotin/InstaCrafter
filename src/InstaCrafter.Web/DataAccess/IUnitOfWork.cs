using InstaBackup.DataAccess.Repository;
using InstaBackup.Models;

namespace InstaBackup.DataAccess
{
    public interface IUnitOfWork
    {
        IRepository<InstaStory> StoryRepository { get; }
        IRepository<InstaMediaPost> PostRepository { get; }
        IRepository<InstaUser> UserRepository { get; }
        void SaveChanges();
        void RejectChanges();
        void Dispose();
    }
}