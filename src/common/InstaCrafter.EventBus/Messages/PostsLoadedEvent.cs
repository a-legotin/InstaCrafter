using System;
using System.Collections;
using System.Collections.Generic;
using CozyBus.Core.Messages;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Messages;

namespace InstaCrafter.UserCrafter.IntegrationEvents.Events
{
    public class PostsLoadedEvent : IBusMessage
    {
        public PostsLoadedEvent(string userName, IEnumerable<InstagramPost> posts)
        {
            UserName = userName;
            Posts = posts;
        }
        public string UserName { get; }
        public IEnumerable<InstagramPost> Posts { get; }
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}