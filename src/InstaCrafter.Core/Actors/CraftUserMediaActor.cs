using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                var userMedia = instaApi.GetUserPosts(int.MaxValue);

            });
        }
    }
}
