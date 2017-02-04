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
           var job = new CraftMediaJob() {UserName = username};
            var repo = new CrafterRepository();
            var logger = new LoggersRepository().GetWebLogger();
            var crafter = repo.GetUserMediaCrafter(job, logger);
            Task.Run(() => { crafter.Craft(); });
        }
    }
}