using InstaCrafter.Core.CrafterJobs;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class CraftJobProgressHub : Hub
    {
        public void ReportJobStarted(ICraftJob job)
        {
            Clients.Caller.reportJobStarted(job);
        }

        public void ReportJobProgress(ICraftJob job)
        {
            Clients.Caller.reportJobProgress(job);
        }
    }
}