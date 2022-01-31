using CozyBus.Core.Handlers;
using CozyBus.RabbitMQ.Extensions;
using InstaCrafter.Accounts.IntegrationEvents.Handlers;
using InstaCrafter.Classes;
using InstaCrafter.EventBus.Messages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InstaCrafter.Accounts
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureServices(ConfigureServices);
        }

        private static void ConfigureServices(HostBuilderContext hostBuilderContext, IServiceCollection services)
        {
            var configuration = hostBuilderContext.Configuration;

            var rabbitOptions = new RabbitOptions();
            configuration.GetSection(nameof(RabbitOptions)).Bind(rabbitOptions);
            services.UseRabbitMqMessageBus(builder =>
            {
                builder.WithConnection(rabbitOptions.Url)
                    .WithUsername(rabbitOptions.Username)
                    .WithPassword(rabbitOptions.Password)
                    .WithPort(rabbitOptions.Port)
                    .WithQueueName(rabbitOptions.QueueName);
                    });
            services.AddHostedService<Worker>();
            services.AddSingleton<IBusMessageHandler<AuthenticateUserMessage>, AddIgAccountEventHandler>();
        }
    }
}