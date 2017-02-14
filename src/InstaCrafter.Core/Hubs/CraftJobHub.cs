using System.Threading.Tasks;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Crafters;
using InstaCrafter.Core.Loggers;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class CraftJobHub : Hub
    {
        public void SendCraftMediaJob(string username)
        {
            var job = CraftJobFactory.Instance.GetNewMediaJob(username);
            var logger = new LoggersFactory().GetWebLogger();
            var crafter = CrafterFactory.Instance.GetUserMediaCrafter(job, logger);
            Task.Run(() => { crafter.Craft(); });
        }
    }
}