using System;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.RabbitMQ;
using InstaCrafter.UserCrafter.UserProviders;
using InstaSharper.Classes.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace InstaCrafter.UserCrafter
{
    class Program
    {
        private static IConfiguration _config;

        static async Task Main(string[] args)
        {
            await ConfigureMapper();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Async(w=>w.File("log.txt", rollingInterval: RollingInterval.Day))
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("http://localhost:9200"))
                {
                    MinimumLogEventLevel = LogEventLevel.Verbose,
                })                
                .WriteTo.ColoredConsole( 
                    LogEventLevel.Verbose,
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            var builder = new HostBuilder()
                .ConfigureHostConfiguration(config =>
                {
                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }
                })
                .ConfigureAppConfiguration((hostContext, config) =>
                {
                    config.AddJsonFile("instasharper.secret.json", optional: true, reloadOnChange: true);
                    config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                    config.AddEnvironmentVariables();

                    if (args != null)
                    {
                        config.AddCommandLine(args);
                    }

                    _config = config.Build();
                })
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
                    services.AddSingleton<IUserDataProvider, InstasharperUserProvider>();
                    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                    {
                        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                        var factory = new ConnectionFactory()
                        {
                            HostName = _config["EventBusConnection"]
                        };

                        if (!string.IsNullOrEmpty(_config["EventBusUserName"]))
                        {
                            factory.UserName = _config["EventBusUserName"];
                        }

                        if (!string.IsNullOrEmpty(_config["EventBusPassword"]))
                        {
                            factory.Password = _config["EventBusPassword"];
                        }

                        return new DefaultRabbitMQPersistentConnection(factory, logger);
                    });

                    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                    {
                        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                        var subscriptionClientName = _config["SubscriptionClientName"];
                        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager,
                            iLifetimeScope, subscriptionClientName);
                    });

                    services.Configure<InstaSharperConfig>(hostContext.Configuration.GetSection("InstaSharperConfig"));
                    services.AddHostedService<UserCrafterService>();
                })
                .UseConsoleLifetime();

            await builder.RunConsoleAsync();
        }

        static async Task ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InstaUser, InstagramUser>();
                cfg.CreateMap<InstaUserShort, InstagramUser>();
            });
        }
    }
}