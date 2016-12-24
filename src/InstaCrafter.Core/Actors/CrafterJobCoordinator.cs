using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Akka.Actor;
using Akka.Event;
using InstaCrafter.Core.CrafterJobs;
using Newtonsoft.Json;

namespace InstaCrafter.Core.Actors
{
    public class OrderProcessorActor : UntypedActor
    {
        #region Message types
        public class QueueJob
        {
            public void Queue(ICraftJob job)
            {
                Job = job;
            }

            public ICraftJob Job { get; private set; }
        }

        public class DeQueueJob
        {
            public void DeQueue(ICraftJob job)
            {
                Job = job;
            }

            public ICraftJob Job { get; private set; }
        }

        #endregion

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
    }
}
