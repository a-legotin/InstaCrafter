using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InstaCrafter.Core.Crafters;
using InstaCrafter.Core.Hubs;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Loggers
{
    public class LoggersRepository
    {
        public ICraftLogger GetWebLogger()
        {
            var hub = GlobalHost.ConnectionManager.GetHubContext<CrafterLogsHub>();
            return new WebLogger(hub);
        }
    }
}