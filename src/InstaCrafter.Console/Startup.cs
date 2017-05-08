using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Database;
using InstaCrafter.Console.Providers;
using InstaCrafter.Console.Providers.PostgreSQL;
using InstaSharper.Classes.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InstaCrafter.Console
{
    public class Startup
    {
        private const string dbConnection =
            @"User ID=alex;Password=alex;Host=localhost;Port=5432;Database=instacrafter;Pooling=true;";

        public static IConfigurationRoot Configuration { get; set; }

        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InstaCrafterPgsqlContext>(options =>
                options.UseNpgsql(
                    dbConnection,
                    b => b.MigrationsAssembly("InstaCrafter.Console")
                )
            );

            services.AddScoped<IDataAccessProvider<InstaMediaDb>, InstaPostsRepository>();
            services.AddScoped<IDataAccessProvider<InstaUserDb>, InstaUsersRepository>();
            services.AddSingleton<Program>();
        }

        public static void Main(string[] args)
        {
            var services = new ServiceCollection();
            var builder = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();
            Configuration = builder.Build();
            ConfigureServices(services);
            var provider = services.BuildServiceProvider();

            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InstaMedia, InstaMediaDb>();
                cfg.CreateMap<InstaUser, InstaUserDb>();
                cfg.CreateMap<InstaMediaList, List<InstaMediaDb>>();
            });

            var ctSource = new CancellationTokenSource();
            var ct = ctSource.Token;

            var task = Task.Run(async () =>
            {
                var program = provider.GetService<Program>();
                await program.Run(ct);
            }, ct);
            try
            {
                task.Wait(ct);
            }
            catch (AggregateException e)
            {
                throw e.InnerException;
            }
            ctSource.Cancel();
            ctSource.Dispose();
        }
    }
}