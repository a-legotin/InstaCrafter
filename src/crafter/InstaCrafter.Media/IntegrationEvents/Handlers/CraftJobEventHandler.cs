using System;
using System.Threading.Tasks;
using CozyBus.Core.Bus;
using CozyBus.Core.Handlers;
using InstaCrafter.EventBus.Messages;
using InstaCrafter.Media.MediaProviders;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class  CraftJobEventHandler : IBusMessageHandler<NewCrafterJobMessage>
    {
        private readonly ILogger<UserLoadEventHandler> _logger;
        private readonly IMediaDataProvider _dataProvider;
        private readonly IMessageBus _eventBus;

        public CraftJobEventHandler(ILogger<UserLoadEventHandler> _logger, IMediaDataProvider _dataProvider, IMessageBus eventBus)
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
                _eventBus.Publish<PostsLoadedEvent>(new PostsLoadedEvent(userLoadedMessage.User, posts));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Id);
            }
        }
    }
}