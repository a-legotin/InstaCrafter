using System.Threading.Tasks;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.UserService.IntegrationEvents.Events;

namespace InstaCrafter.UserService.IntegrationEvents.EventHandlers
{
    public class UserLoadedEventHandler : IIntegrationEventHandler<UserLoadedEvent>
    {
        public async Task Handle(UserLoadedEvent userLoadedEvent)
        {
            
        }
    }
}