using InstaCrafter.EventBus.Events;
using Newtonsoft.Json;

namespace InstaCrafter.UserService.IntegrationEvents.Events
{
    public class UserLoadedEvent : IntegrationEvent
    {
        public UserLoadedEvent(long userId)
        {
            UserId = userId;
        }

        [JsonProperty]
        public long UserId { get; set; }
    }
}