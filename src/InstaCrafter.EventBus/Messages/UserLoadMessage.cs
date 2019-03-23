using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class UserLoadMessage : IntegrationMessage
    {
        public UserLoadMessage(InstagramUser user)
        {
            User = user;
        }

        public InstagramUser User { get; }
    }
}