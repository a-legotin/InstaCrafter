using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Routing;
using Consul;

namespace InstaCrafter.Core
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected async void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var client = new ConsulClient();
            var serviceList = await client.Catalog.Service("instacrafter.datastore");
            var serv = serviceList.Response;
        }
    }
}
