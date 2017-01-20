using System.IO;
using System.Net;
using System.Threading.Tasks;
using Consul;
using Microsoft.AspNetCore.Hosting;

namespace InstaCrafter
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var discoveryRegisterSucceed = RegisterService();
            if (!discoveryRegisterSucceed.Result) return;
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();
            host.Run();
        }

        private static async Task<bool> RegisterService()
        {
            var client = new ConsulClient();
            var svcID = "instacrafter.datastore";
            var service = new AgentService
            {
                ID = svcID,
                Service = "instacrafter.datastore",
                Tags = new[] {"master", "v1"},
                Port = 5000
            };


            var registration = new CatalogRegistration
            {
                Datacenter = "dc1",
                Node = "AL-LAPTOP",
                Address = "192.168.1.150",
                Service = service
            };

            var registerResult = await client.Catalog.Register(registration);
            return registerResult.StatusCode == HttpStatusCode.OK;
        }
    }
}