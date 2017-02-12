using System.Collections.Generic;

namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftJobRepository
    {
        private static CraftJobRepository _instance;
        private static readonly object JobLock = new object();

        private CraftJobRepository()
        {
            Jobs = new List<ICraftJob>();
        }

        public List<ICraftJob> Jobs { get; }

        public static CraftJobRepository Instance
        {
            get
            {
                lock (JobLock)
                {
                    return _instance ?? (_instance = new CraftJobRepository());
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