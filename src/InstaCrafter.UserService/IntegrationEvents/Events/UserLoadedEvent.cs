using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Events;
using Newtonsoft.Json;

namespace InstaCrafter.UserService.IntegrationEvents.Events
{
    public class UserLoadedEvent : IntegrationEvent
    {
        public UserLoadedEvent(InstagramUser user)
        {
            User = user;
        }

        [JsonProperty]
        public InstagramUser User { get; }
    }
}