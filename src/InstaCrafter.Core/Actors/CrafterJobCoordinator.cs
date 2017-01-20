using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;

namespace InstaCrafter.Core.Actors
{
    public class OrderProcessorActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            if (message is CraftMediaJob)
            {
                var msg = message as CraftMediaJob;
                Context.ActorOf(Props.Create(() => new CraftUserMediaActor()));
            }
            if (message is CraftUserJob)
            {
                var msg = message as CraftUserJob;
                Context.ActorOf(Props.Create(() => new CraftUsersActor()));
            }
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