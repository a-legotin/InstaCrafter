using System;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using InstaCrafter.UserService.DataProvider;
using InstaCrafter.UserService.DtoModels;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class UserLoadedEventHandler : IIntegrationEventHandler<UserLoadedEvent>
    {
        private readonly IDataAccessProvider<InstagramUserDto> _repo;
        private readonly ILogger<UserLoadedEventHandler> _logger;

        public UserLoadedEventHandler(IDataAccessProvider<InstagramUserDto> repo, ILogger<UserLoadedEventHandler> _logger)
        {
            _repo = repo;
            this._logger = _logger;
        }

        public async Task Handle(UserLoadedEvent userLoadedEvent)
        {
            _logger.LogDebug($"Got an event! User: '{userLoadedEvent.User.UserName}'");
            try
            {
                var user = userLoadedEvent.User;
                var dtoUser = Mapper.Map<InstagramUserDto>(user);
                if (_repo.Exist(dtoUser))
                {
                    var existingUser = _repo.Get(dtoUser.UserName);
                    _repo.Update(existingUser.Id, dtoUser);
                    _logger.LogDebug($"User: '{userLoadedEvent.User.UserName}' updated");
                }
                else
                {
                    _repo.Add(dtoUser);
                    _logger.LogDebug($"User: '{userLoadedEvent.User.UserName}' created");
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedEvent.Guid);
            }
        }
    }
}