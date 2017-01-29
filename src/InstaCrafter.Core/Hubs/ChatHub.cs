using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(string name, string message)
        {
            Clients.All.addNewMessageToPage(name, message);
        }

        public void SendJob(string username)
        {
        }
    }
}