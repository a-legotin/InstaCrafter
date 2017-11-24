using InstaCrafter.Web.DataAccess.Repository;
using InstaCrafter.Web.Models;

namespace InstaCrafter.Web.DataAccess
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