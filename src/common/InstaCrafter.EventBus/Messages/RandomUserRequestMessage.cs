using System;
using CozyBus.Core.Messages;

namespace InstaCrafter.EventBus.Messages
{
    public class RandomUserRequestMessage : IBusMessage
    {
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}