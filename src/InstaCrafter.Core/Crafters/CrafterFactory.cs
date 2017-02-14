using System.Collections.Generic;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Hubs;
using InstaCrafter.Core.Loggers;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Crafters
{
    public class CrafterFactory
    {
        private static CrafterFactory _instance;
        private static readonly object CrafterLock = new object();

        private CrafterFactory()
        {
            Crafters = new List<ICrafter>();
        }

        public List<ICrafter> Crafters { get; }

        public static CrafterFactory Instance
        {
            get
            {
                lock (CrafterLock)
                {
                    return _instance ?? (_instance = new CrafterFactory());
                }
            }
        }

        public ICrafter GetUserMediaCrafter(CraftMediaJob job, ICraftLogger craftLogger)
        {
            var logger = new LoggersFactory().GetWebLogger();
            var progressReporter = GlobalHost.ConnectionManager.GetHubContext<CraftJobProgressHub>();
            var crafter = new UserMediaCrafter(logger, job, Crafters.Count + 1, progressReporter);
            Crafters.Add(crafter);
            return crafter;
        }
    }
}