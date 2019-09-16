using System;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.EventBus.Messages;
using InstaCrafter.Extensions;
using InstaCrafter.Media.MediaProviders;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using InstaCrafter.UserService.IntegrationEvents.EventHandlers;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace InstaCrafter.Media
{
    public class MediaCrafterService : BackgroundService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;

        public MediaCrafterService(IEventBus eventBus, ILogger<MediaCrafterService> logger)
        {
            _eventBus = eventBus;
            _eventBus.Subscribe<UserLoadMessage, UserLoadEventHandler>();
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Approaching users for posts & stories");
            try
            {
                for (int i = 0; i < 20; i++)
                {
                    _eventBus.Publish(new RandomUserRequestMessage());
                    await Task.Delay(TimeSpan.FromSeconds(120));
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error in executing task");
            }

            _logger.LogDebug("Task completed");
        }
    }
}