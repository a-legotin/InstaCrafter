using System.Collections.Generic;
using System.Threading.Tasks;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.Media.MediaProviders
{
    public interface IMediaDataProvider
    {
        Task<IEnumerable<InstagramPost>> GetUserPostsFollowers(string username);
        Task<InstagramReelFeed> GetUserStory(string username);
    }
}