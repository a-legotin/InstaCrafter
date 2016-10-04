using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Models;

namespace InstaCrafter
{
    public interface IDataAccessProvider
    {
        void AddPost(InstaPost post);
        void UpdatePost(long postId, InstaPost post);
        void DeletePost(long postId);
        InstaPost GetPost(long postId);
        List<InstaPost> GetPosts();
    }
}
