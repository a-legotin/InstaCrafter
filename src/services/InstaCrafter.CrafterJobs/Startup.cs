using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using InstaCrafter.CrafterJobs.DataProvider;
using InstaCrafter.CrafterJobs.DataProvider.PostgreSQL;
using InstaCrafter.CrafterJobs.DtoModels;
using InstaCrafter.CrafterJobs.HostedServices;
using InstaCrafter.EventBus;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.RabbitMQ;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.Elasticsearch;

namespace InstaCrafter.CrafterJobs
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().AddNewtonsoftJson();

            var elasticSearchConnection = Configuration["ElasticConnection"];
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Verbose()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Verbose)
                .Enrich.FromLogContext()
                .WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(elasticSearchConnection))
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
                    b => b.MigrationsAssembly("InstaCrafter.CrafterJobs")
                )
            );


            services.AddScoped<IDataAccessProvider<InstaCrafterJobDto>, CrafterJobsRepository>();
            services.AddHostedService<TimedHostedService>();

            
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
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseRouting(routes => { routes.MapApplication(); });

            app.UseAuthorization();
        }
    }
}