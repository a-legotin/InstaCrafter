using System;
using CozyBus.Core.Messages;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class UserLoadedMessage : IBusMessage
    {
        public UserLoadedMessage(InstagramUser user)
        {
            User = user;
        }

        public InstagramUser User { get; }
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}