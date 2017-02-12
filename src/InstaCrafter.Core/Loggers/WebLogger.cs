using System;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Loggers
{
    public class WebLogger : ICraftLogger
    {
        private readonly IHubContext _hubContext;

        public WebLogger(IHubContext hubContext)
        {
            _hubContext = hubContext;
        }

        public void WriteLog(LogMessageType messageType, string message)
        {
            var logString = ComposeLogString(message);
            _hubContext.Clients.All.notify(logString);
        }

        private object ComposeLogString(string message)
        {
            return $"[{DateTime.Now.ToShortTimeString()}]: {message}";
        }
    }
}