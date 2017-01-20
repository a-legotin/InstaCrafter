using System.Web;
using System.Web.Http;
using Consul;

namespace InstaCrafter.Core
{
    public class WebApiApplication : HttpApplication
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