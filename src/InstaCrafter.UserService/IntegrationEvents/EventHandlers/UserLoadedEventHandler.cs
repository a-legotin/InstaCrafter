using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.UserService.DataProvider;
using InstaCrafter.UserService.DtoModels;
using InstaCrafter.UserService.IntegrationEvents.Events;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class UserLoadedEventHandler : IIntegrationEventHandler<UserLoadedEvent>
    {
        private readonly IDataAccessProvider<InstagramUserDto> _repo;

        public UserLoadedEventHandler(IDataAccessProvider<InstagramUserDto> repo)
        {
            _repo = repo;
        }

        public async Task Handle(UserLoadedEvent userLoadedEvent)
        {
            var user = userLoadedEvent.User;
            var dtoUser = Mapper.Map<InstagramUserDto>(user);
            if (_repo.Exist(dtoUser))
            {
                var existingUser = _repo.Get(dtoUser.UserName);
                _repo.Update(existingUser.Id, dtoUser);
            }
            _repo.Add(dtoUser);
        }
    }
}