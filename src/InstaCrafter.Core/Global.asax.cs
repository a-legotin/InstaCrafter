using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http;
using Consul;
using InstaCrafter.Classes.Database;
using InstagramApi.API;
using Newtonsoft.Json;

namespace InstaCrafter.Core
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var workerThread = new Thread(() =>
            {
                var worker = new InstaWorkerSimple("alexandr_le");
                worker.Craft();
            });
            workerThread.Start();
        }
    }
}