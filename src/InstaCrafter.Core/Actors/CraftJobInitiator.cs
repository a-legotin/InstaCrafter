using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;

namespace InstaCrafter.Core.Actors
{
    public class CraftJobInitiator : UntypedActor
    {
        public class InitialCraftJob
        {
            public InitialCraftJob(string instaUserName)
            {
                InstaUserName = instaUserName;
            }

            public string InstaUserName { get; private set; }
        }
        protected override void OnReceive(object message)
        {
            if (!(message is InitialCraftJob))
                Unhandled(message);

            var initialJob = message as InitialCraftJob;
            if(initialJob == null) return;
            var job = new CraftUserJob() {UserName = initialJob.InstaUserName};
            IActorRef jobCoordinator = Context.ActorOf(Props.Create(() => new CrafterJobCoordinator()));
            jobCoordinator.Tell(job);
        }
    }
}