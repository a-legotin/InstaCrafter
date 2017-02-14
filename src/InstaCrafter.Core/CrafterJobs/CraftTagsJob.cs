using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftTagsJob : ICraftJob
    {
        public CraftJobKind Kind { get; }
        public int Id { get; }
        public CraftJobProgress Progress { get; set; }
    }
}