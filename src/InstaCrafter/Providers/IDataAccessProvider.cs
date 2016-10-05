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
        void UpdatePost(int postId, InstaPost post);
        void DeletePost(int postId);
        InstaPost GetPost(int postId);
        List<InstaPost> GetPosts();
    }
}
