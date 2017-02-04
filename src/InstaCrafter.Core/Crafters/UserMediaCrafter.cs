using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Loggers;
using InstagramApi.API;
using InstagramApi.Classes;

namespace InstaCrafter.Core.Crafters
{
    public class UserMediaCrafter : ICrafter
    {
        public UserMediaCrafter(ICraftLogger logger, ICraftJob job,  int id)
        {
            Logger = logger;
            Id = id;
            Job = job;
        }

        public void Craft()
        {
            var craftUserMediaJob = Job as CraftMediaJob;
            if (craftUserMediaJob == null)
            {
                Logger.WriteLog(LogMessageType.Error, $"#{Id} craft job not defined, terminating");
                return;
            }
            var instaApi = new InstaApiBuilder().Build();
            var userMedia = instaApi.GetUserPostsByUsername(craftUserMediaJob.UserName, craftUserMediaJob.PagesCount);
            if (userMedia == null || userMedia.Count < 1)
            {
                Logger.WriteLog(LogMessageType.Warning, $"#{Id} there is no media for user {craftUserMediaJob.UserName}, terminating");
                return;
            }
            foreach (var media in userMedia)
            {
                Thread.Sleep(5000);
                Logger.WriteLog(LogMessageType.Info, $"#{Id} processing post: {media.Code} for user {craftUserMediaJob.UserName}");
            }
        }

        public ICraftJob Job { get; set; }
        public ICraftLogger Logger { get; }
        public string Name { get; set; } = "User media crafter";
        public int Id { get; }
    }
}