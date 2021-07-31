using System;
using CozyBus.Core.Messages;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class UserLoadMessage : IBusMessage
    {
        public UserLoadMessage(long userId, InstagramUser user)
        {
            User = user;
            UserId = userId;
        }

        public long UserId { get; }

        public InstagramUser User { get; }
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}