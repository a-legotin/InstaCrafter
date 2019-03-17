using System;
using System.Threading.Tasks;
using InstaCrafter.EventBus;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.RabbitMQ;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;

namespace InstaCrafter.UserCrafter
{
    class Program
    {
        private static IConfiguration _config;
        static async Task Main(string[] args)
        {
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
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

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
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                        var subscriptionClientName = _config["SubscriptionClientName"];
                        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager, subscriptionClientName);
                    });
                    
                    services.Configure<InstaSharperConfig>(hostContext.Configuration.GetSection("InstaSharperConfig"));
                    services.AddHostedService<UserCrafterService>();
                });
            await builder.RunConsoleAsync();
        }
    }
}