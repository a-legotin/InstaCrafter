using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class CraftLogsHub : Hub
    {
        public void Notify(string message)
        {
            Clients.All.notify(message);
        }
    }
}