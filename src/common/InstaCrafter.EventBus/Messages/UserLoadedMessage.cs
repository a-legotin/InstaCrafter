using System;
using CozyBus.Core.Messages;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Messages;

namespace InstaCrafter.UserCrafter.IntegrationEvents.Events
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