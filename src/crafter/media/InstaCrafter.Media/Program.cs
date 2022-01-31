using CozyBus.RabbitMQ.Extensions;
using InstaCrafter.Classes;
using InstaCrafter.Media;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = Host.CreateDefaultBuilder(args)
               .ConfigureServices(ConfigureServices)
               .Build();

await host.RunAsync();


void ConfigureServices(HostBuilderContext hostBuilderContext,
                       IServiceCollection services)
{
    var configuration = hostBuilderContext.Configuration;

    var rabbitOptions = new RabbitOptions();
    configuration.GetSection(nameof(RabbitOptions)).Bind(rabbitOptions);
    services.UseRabbitMqMessageBus(builder =>
    {
        builder.WithConnection(rabbitOptions.Url)
               .WithUsername(rabbitOptions.Username)
               .WithPassword(rabbitOptions.Password)
               .WithPort(rabbitOptions.Port);
    });
    services.AddHostedService<Worker>();
}