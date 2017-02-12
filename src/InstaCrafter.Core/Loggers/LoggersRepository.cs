using InstaCrafter.Core.Hubs;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Loggers
{
    public class LoggersRepository
    {
        public ICraftLogger GetWebLogger()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<CraftLogsHub>();
            return new WebLogger(hub);
        }
    }
}