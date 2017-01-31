using InstaCrafter.Core;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Startup))]

namespace InstaCrafter.Core
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var config = new HubConfiguration {EnableJavaScriptProxies = true};
            app.MapSignalR(config);
        }
    }
}