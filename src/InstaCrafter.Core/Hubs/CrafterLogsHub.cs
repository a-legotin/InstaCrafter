using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class CrafterLogsHub : Hub
    {
        public void Notify(string message)
        {
            Clients.All.notify(message);
        }
    }
}