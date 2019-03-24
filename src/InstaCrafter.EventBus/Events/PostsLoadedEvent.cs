using System.Collections;
using System.Collections.Generic;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Messages;

namespace InstaCrafter.UserCrafter.IntegrationEvents.Events
{
    public class PostsLoadedEvent : IntegrationMessage
    {
        public PostsLoadedEvent(long userId, InstagramUser user, IEnumerable<InstagramPost> posts)
        {
            UserId = userId;
            User = user;
            Posts = posts;
        }

        public long UserId { get; }
        public InstagramUser User { get; }
        public IEnumerable<InstagramPost> Posts { get; }
    }
}