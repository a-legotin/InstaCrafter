using System.Collections.Generic;

namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftJobFactory
    {
        private static CraftJobFactory _instance;
        private static readonly object JobLock = new object();

        private CraftJobFactory()
        {
            Jobs = new List<ICraftJob>();
        }

        public List<ICraftJob> Jobs { get; }

        public static CraftJobFactory Instance
        {
            get
            {
                lock (JobLock)
                {
                    return _instance ?? (_instance = new CraftJobFactory());
                }
            }
        }

        public CraftMediaJob GetNewMediaJob(string username)
        {
            var newJob = new CraftMediaJob(username, Jobs.Count + 1);
            Jobs.Add(newJob);
            return newJob;
        }
    }
}