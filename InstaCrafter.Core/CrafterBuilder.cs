using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public class CrafterBuilder
    {
        internal static ICrafter<InstaPostList> GetCrafter()
        {
            return new InstaPostsCrafter(@"https://www.instagram.com/alexandr_le/", new System.Threading.CancellationToken());
        }
    }
}
