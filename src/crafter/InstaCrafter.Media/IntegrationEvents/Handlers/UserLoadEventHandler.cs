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
    public class UserLoadEventHandler : IBusMessageHandler<UserLoadMessage>
    {
        private readonly ILogger<UserLoadEventHandler> _logger;
        private readonly IMediaDataProvider _dataProvider;
        private readonly IMessageBus _eventBus;

        public UserLoadEventHandler(ILogger<UserLoadEventHandler> _logger, IMediaDataProvider _dataProvider, IMessageBus eventBus)
        {
            this._logger = _logger;
            this._dataProvider = _dataProvider;
            _eventBus = eventBus;
        }

        public async Task Handle(UserLoadMessage userLoadedMessage)
        {
            _logger.LogDebug($"Got an event! User: '{userLoadedMessage.User.UserName}'");
            try
            {
                var posts = await _dataProvider.GetUserPosts(userLoadedMessage.User.UserName);
                _eventBus.Publish<UserLoadedMessage>(new UserLoadedMessage(userLoadedMessage.User));
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Id);
            }
        }
    }
}