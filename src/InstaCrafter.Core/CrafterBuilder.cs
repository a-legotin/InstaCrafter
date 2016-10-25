using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstaCrafter.Core.Crafters;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public class CrafterBuilder
    {
        internal static ICrafter<InstaPostList> GetPostsCrafter(string postSource)
        {
            return new PostCrafter(postSource, "http://localhost:5000/api/post", new System.Threading.CancellationToken());
        }

        internal static object GetUsersCrafter()
        {
            return new UserCrafter("http://localhost:5000/api/user", new System.Threading.CancellationToken());
        }
    }
}
