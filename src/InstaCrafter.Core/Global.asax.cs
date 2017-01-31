using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using InstaCrafter.Core.Hubs;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var myHub = GlobalHost.ConnectionManager.GetHubContext<CrafterLogsHub>();

            var thread = new Thread(() =>
            {
                Thread.Sleep(5000);
                for (int i = 0; i < 80; i++)
                {
                    var result = myHub.Clients.All.notify("test" + i);
                    Thread.Sleep(100);
                }
            });
            thread.Start();
        }
    }
}