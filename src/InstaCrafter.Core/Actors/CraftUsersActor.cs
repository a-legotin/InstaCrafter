using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;
using InstagramApi.API;

namespace InstaCrafter.Core.Actors
{
    public class CraftUsersActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            var craftUserJob = message as CraftUserJob;
            if(craftUserJob == null) return;
            var api = new InstaApiBuilder().SetUserName(craftUserJob.UserName).Build();
            var user = api.GetUser();
        }
    }
}