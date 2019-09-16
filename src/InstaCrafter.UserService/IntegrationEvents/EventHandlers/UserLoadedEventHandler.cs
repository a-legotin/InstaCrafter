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
    public class UserLoadedEventHandler : IIntegrationEventHandler<UserLoadedMessage>
    {
        private readonly IDataAccessProvider<InstagramUserDto> _repo;
        private readonly ILogger<UserLoadedEventHandler> _logger;

        public UserLoadedEventHandler(IDataAccessProvider<InstagramUserDto> repo, ILogger<UserLoadedEventHandler> _logger)
        {
            _repo = repo;
            this._logger = _logger;
        }

        public async Task Handle(UserLoadedMessage userLoadedMessage)
        {
            _logger.LogDebug($"Got an event! User: '{userLoadedMessage.User.UserName}'");
            try
            {
                var user = userLoadedMessage.User;
                var dtoUser = Mapper.Map<InstagramUserDto>(user);
                if (_repo.Exist(dtoUser))
                {
                    var existingUser = _repo.Get(dtoUser.UserName);
                    _repo.Update(existingUser.Id, dtoUser);
                    _logger.LogDebug($"User: '{userLoadedMessage.User.UserName}' updated");
                }
                else
                {
                    _repo.Add(dtoUser);
                    _logger.LogDebug($"User: '{userLoadedMessage.User.UserName}' created");
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", userLoadedMessage.Guid);
            }
        }
    }
}