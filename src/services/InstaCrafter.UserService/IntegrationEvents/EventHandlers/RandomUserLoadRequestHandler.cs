using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.EventBus.Messages;
using InstaCrafter.Extensions;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using InstaCrafter.UserService.DataProvider;
using InstaCrafter.UserService.DtoModels;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class RandomUserLoadRequestHandler : IIntegrationEventHandler<RandomUserRequestMessage>
    {
        private readonly IDataAccessProvider<InstagramUserDto> _repo;
        private readonly ILogger<RandomUserLoadRequestHandler> _logger;
        private readonly IEventBus _eventBus;

        public RandomUserLoadRequestHandler(IDataAccessProvider<InstagramUserDto> repo,
            ILogger<RandomUserLoadRequestHandler> logger,
            IEventBus eventBus)
        {
            _repo = repo;
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task Handle(RandomUserRequestMessage userLoadedMessage)
        {
            try
            {
                await Task.Run(() =>
                {
                    var user = _repo.GetItems().Randomize().FirstOrDefault();
                    if (user == null)
                    {
                        _logger.LogInformation("No users available yet");
                        return;
                    }
                    _eventBus.Publish(new UserLoadMessage(user.Id, Mapper.Map<InstagramUser>(user)));
                });
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Guid);
            }
        }
    }
}