using System;
using System.Threading.Tasks;
using CozyBus.Core.Bus;
using CozyBus.Core.Handlers;
using InstaCrafter.EventBus.Messages;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Accounts.IntegrationEvents.Handlers
{
    public class AddIgAccountEventHandler : IBusMessageHandler<AuthenticateUserMessage>
    {
        private readonly IMessageBus _eventBus;
        private readonly ILogger<AddIgAccountEventHandler> _logger;

        public AddIgAccountEventHandler(ILogger<AddIgAccountEventHandler> _logger, IMessageBus eventBus)
        {
            this._logger = _logger;
            _eventBus = eventBus;
        }

        public async Task Handle(AuthenticateUserMessage userLoadedMessage)
        {
            _logger.LogDebug($"Got an event! User: '{userLoadedMessage.Username}'");
            try
            {
                await Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Id);
            }
        }
    }
}