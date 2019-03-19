using System.Collections.Generic;
using System.Threading.Tasks;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.UserCrafter.UserProviders
{
    public interface IUserDataProvider
    {
        Task<InstagramUser> GetUser(string username);
        Task<IEnumerable<InstagramUser>> GetUserFollowings(string username);
        Task<IEnumerable<InstagramUser>> GetUserFollowers(string username);
    }
}