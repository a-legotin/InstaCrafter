using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using InstaCrafter.Classes;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.Media.MediaProviders;
using InstaCrafter.RabbitMQ;
using InstaCrafter.UserService.IntegrationEvents.EventHandlers;
using InstaSharper.Classes.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;
using InstaReelFeed = InstaSharper.Classes.Models.InstaReelFeed;

namespace InstaCrafter.Media
{
    class Program
    {
        private static IConfiguration Configuration;

        static async Task Main(string[] args)
        {
            await ConfigureMapper();

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

                    Configuration = config.Build();
                })
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddOptions();
                    services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
                    services.AddSingleton<IMediaDataProvider, InstasharperMediaProvider>();
                    services.AddSingleton<IRabbitMQPersistentConnection>(sp =>
                    {
                        var logger = sp.GetRequiredService<ILogger<DefaultRabbitMQPersistentConnection>>();
                        var factory = new ConnectionFactory()
                        {
                            HostName = Configuration["EventBusConnection"]
                        };

                        if (!string.IsNullOrEmpty(Configuration["EventBusUserName"]))
                        {
                            factory.UserName = Configuration["EventBusUserName"];
                        }

                        if (!string.IsNullOrEmpty(Configuration["EventBusPassword"]))
                        {
                            factory.Password = Configuration["EventBusPassword"];
                        }

                        return new DefaultRabbitMQPersistentConnection(factory, logger);
                    });

                    services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
                    {
                        var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                        var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                        var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                        var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                        var subscriptionClientName = Configuration["SubscriptionClientName"];
                        return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager,
                            iLifetimeScope, subscriptionClientName);
                    });

                    services.Configure<InstaSharperConfig>(hostContext.Configuration.GetSection("InstaSharperConfig"));
                    services.AddHostedService<MediaCrafterService>();
                    services.AddTransient<UserLoadEventHandler>();
                    services.AddSingleton<IImageLoader, ImageLoader>();
                })
                .UseConsoleLifetime();
            
            var elasticSearchConnection = Configuration["ElasticConnection"];
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchConnection))
                {
                    MinimumLogEventLevel = LogEventLevel.Verbose
                })                
                .WriteTo.ColoredConsole( 
                    LogEventLevel.Verbose,
                    "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            await builder.RunConsoleAsync();
        }

        static async Task ConfigureMapper()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<InstaUser, InstagramUser>();
                cfg.CreateMap<InstaUserShort, InstagramUser>();
                cfg.CreateMap<InstaImage, InstagramImage>();
                cfg.CreateMap<InstaVideo, InstagramVideo>();
                cfg.CreateMap<InstaReelFeed, InstagramReelFeed>();
                cfg.CreateMap<InstaStoryItem, InstagramStoryItem>();
                cfg.CreateMap<InstaMediaType, InstagramMediaType>();
                cfg.CreateMap<InstaCaption, InstagramCaption>();
                cfg.CreateMap<InstaLocation, InstagramLocation>();
                cfg.CreateMap<InstaCarousel, InstagramCarousel>();
                cfg.CreateMap<InstaCarouselItem, InstagramCarouselItem>();
                cfg.CreateMap<InstaMedia, InstagramPost>();

                cfg.AllowNullCollections = true;
            });
        }
    }
}