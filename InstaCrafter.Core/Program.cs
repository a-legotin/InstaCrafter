using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Builder;
using System.Threading;

namespace InstaCrafter.Core
{
    public class Program
    {

        public static void Main(string[] args)
        {

            var userList = new string[] { "alexandr_le" };

            var crafters = new List<Thread>();
            foreach (var user in userList)
            {
                crafters.Add(new Thread(new ThreadStart(() =>
                {
                    var crafter = CrafterBuilder.GetPostsCrafter(user);
                    crafter.Craft();
                })));
            }

            foreach (var crafter in crafters)
            {
                crafter.Start();
            }

            var host = new WebHostBuilder()
                .UseKestrel()
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .Build();

            host.Run();
        }
    }
}
