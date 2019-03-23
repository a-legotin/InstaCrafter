using System;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.Extensions;
using InstaCrafter.Media.MediaProviders;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace InstaCrafter.Media
{
    public class MediaCrafterService : BackgroundService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly IMediaDataProvider _mediaProvider;

        public MediaCrafterService(IEventBus eventBus, IMediaDataProvider mediaProvider,
            ILogger<MediaCrafterService> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
            _mediaProvider = mediaProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Executing media  loading task");
            try
            {
                var story = await _mediaProvider.GetUserStory("lenatemnikovaofficial");
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error in executing task");
            }

            _logger.LogDebug("Task completed");
        }
    }
}