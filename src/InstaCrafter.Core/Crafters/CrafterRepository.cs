using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Loggers;

namespace InstaCrafter.Core.Crafters
{
    public class CrafterRepository
    {
        public List<ICrafter> Crafters { get; } = new List<ICrafter>();
        public ICrafter GetUserMediaCrafter(CraftMediaJob job, ICraftLogger craftLogger)
        {
            var logger = new LoggersRepository().GetWebLogger();
            var crafter = new UserMediaCrafter(logger, job, Crafters.Count + 1);
            Crafters.Add(crafter);
            return crafter;
        }
    }
}