using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;

namespace InstaCrafter.Core.Actors
{
    public class CrafterJobCoordinator : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            IActorRef craftActor = null;
            if (message is CraftMediaJob)
            {
                craftActor = Context.ActorOf(Props.Create(() => new CraftUserMediaActor()));
            }
            if (message is CraftUserJob)
            {
                craftActor = Context.ActorOf(Props.Create(() => new CraftUsersActor()));
            }
           craftActor.Tell(message);
        }

        #region Message types

        public class QueueJob
        {
            public ICraftJob Job { get; private set; }

            public void Queue(ICraftJob job)
            {
                Job = job;
            }
        }

        public class DeQueueJob
        {
            public ICraftJob Job { get; private set; }

            public void DeQueue(ICraftJob job)
            {
                Job = job;
            }
        }

        #endregion
    }
}