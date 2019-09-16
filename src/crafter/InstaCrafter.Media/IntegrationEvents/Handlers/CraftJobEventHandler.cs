using System;
using System.Threading.Tasks;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.EventBus.Messages;
using InstaCrafter.Media.MediaProviders;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class  CraftJobEventHandler : IIntegrationEventHandler<NewCrafterJobMessage>
    {
        private readonly ILogger<UserLoadEventHandler> _logger;
        private readonly IMediaDataProvider _dataProvider;
        private readonly IEventBus _eventBus;

        public CraftJobEventHandler(ILogger<UserLoadEventHandler> _logger, IMediaDataProvider _dataProvider, IEventBus eventBus)
        {
            this._logger = _logger;
            this._dataProvider = _dataProvider;
            _eventBus = eventBus;
        }

        public async Task Handle(NewCrafterJobMessage userLoadedMessage)
        {
            _logger.LogDebug($"Got an event! User: '{userLoadedMessage.User}'");
            try
            {
                var posts = await _dataProvider.GetUserPosts(userLoadedMessage.User);
                _eventBus.Publish(new PostsLoadedEvent(userLoadedMessage.User, posts));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Guid);
            }
        }
    }
}