using System;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.PostService.DataProvider;
using InstaCrafter.PostService.DataProvider.PostgreSQL;
using InstaCrafter.PostService.DtoModels;
using InstaCrafter.PostService.IntegrationEvents.EventHandlers;
using InstaCrafter.RabbitMQ;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace InstaCrafter.PostService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();
            
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
            
            //Use a PostgreSQL database
            var sqlConnectionString = Configuration.GetConnectionString("DataAccessPostgreSqlProvider");

            services.AddDbContext<PostgreSqlDatabaseContext>(options =>
                options.UseNpgsql(
                    sqlConnectionString,
                    b => b.MigrationsAssembly("InstaCrafter.PostService")
                )
            );


            services.AddScoped<IDataAccessProvider<InstagramPostDto>, InstagramPostsRepository>();
            
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
            
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();

            services.AddSingleton<IEventBus, EventBusRabbitMQ>(sp =>
            {
                var rabbitMQPersistentConnection = sp.GetRequiredService<IRabbitMQPersistentConnection>();
                var logger = sp.GetRequiredService<ILogger<EventBusRabbitMQ>>();
                var iLifetimeScope = sp.GetRequiredService<ILifetimeScope>();
                var eventBusSubcriptionsManager = sp.GetRequiredService<IEventBusSubscriptionsManager>();
                var subscriptionClientName = Configuration["SubscriptionClientName"];
                return new EventBusRabbitMQ(rabbitMQPersistentConnection, logger, eventBusSubcriptionsManager,iLifetimeScope, subscriptionClientName);
            });

            services.AddTransient<PostsLoadedEventHandler>();

            Mapper.Initialize(config =>
            {
                config.CreateMap<InstagramPost, InstagramPostDto>();
                config.CreateMap<InstagramPostDto, InstagramPost>();
                
                config.CreateMap<InstagramImage, InstagramImageDto>();
                config.CreateMap<InstagramImageDto, InstagramImage>();
                
                config.CreateMap<InstagramVideo, InstagramVideoDto>();
                config.CreateMap<InstagramVideoDto, InstagramVideo>();
                
                                
                config.CreateMap<InstagramLocation, InstagramLocationDto>();
                config.CreateMap<InstagramLocationDto, InstagramLocation>();
                
                config.CreateMap<InstagramCaption, InstagramCaptionDto>();
                config.CreateMap<InstagramCaptionDto, InstagramCaption>();
                
                config.CreateMap<InstagramCarouselItem, InstagramCarouselItemDto>();
                config.CreateMap<InstagramCarouselItemDto, InstagramCarouselItem>();
            });
            
            var container = new ContainerBuilder();
            container.Populate(services);

            return new AutofacServiceProvider(container.Build());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting(routes => { routes.MapApplication(); });
            ConfigureEventBus(app);
        }
        
        private void ConfigureEventBus(IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<PostsLoadedEvent, PostsLoadedEventHandler>();
        }  
    }
}