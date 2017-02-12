using InstaCrafter.Core.CrafterJobs;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class CraftJobProgressHub : Hub
    {
        public void ReportJobStarted(ICraftJob job)
        {
            Clients.All.reportJobStarted(job);
        }

        public void ReportJobProgress(ICraftJob job)
        {
            Clients.All.reportJobProgress(job);
        }
    }
}