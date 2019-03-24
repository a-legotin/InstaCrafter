using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Messages;
using Newtonsoft.Json;

namespace InstaCrafter.UserCrafter.IntegrationEvents.Events
{
    public class UserLoadedMessage : IntegrationMessage
    {
        public UserLoadedMessage(InstagramUser user)
        {
            User = user;
        }

        [JsonProperty] public InstagramUser User { get; }
    }
}