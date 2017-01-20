using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;
using InstagramApi.API;

namespace InstaCrafter.Core.Actors
{
    public class CraftUserMediaActor : ReceiveActor
    {
        public CraftUserMediaActor()
        {
            Receive<CraftUserJob>(cratJob =>
            {
                var instaApi = new InstaApiBuilder().Build();
                var userMedia = instaApi.GetUserPostsByUsername(cratJob.Username, cratJob.PagesCount);
            });
        }
    }
}