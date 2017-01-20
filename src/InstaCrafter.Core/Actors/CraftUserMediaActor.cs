using Akka.Actor;
using InstaCrafter.Core.CrafterJobs;

namespace InstaCrafter.Core.Actors
{
    public class CraftUserMediaActor : ReceiveActor
    {
        public CraftUserMediaActor()
        {
            Receive<CraftUserJob>(cratJob =>
            {
                
            });
        }
    }
}