using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftUserJob : ICraftJob
    {
        public string Username { get; set; }
        public int PagesCount { get; set; }
    }
}
