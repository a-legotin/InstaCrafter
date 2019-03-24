using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class UserLoadMessage : IntegrationMessage
    {
        public UserLoadMessage( long userId, InstagramUser user)
        {
            User = user;
            UserId = userId;
        }
        
        public long UserId { get; }

        public InstagramUser User { get; }
    }
}