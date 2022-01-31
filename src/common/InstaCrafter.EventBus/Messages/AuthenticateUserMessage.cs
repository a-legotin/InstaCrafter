using System;
using CozyBus.Core.Messages;

namespace InstaCrafter.EventBus.Messages
{
    public class AuthenticateUserMessage : IBusMessage
    {
        public AuthenticateUserMessage(string username, string password)
        {
            Username = username;
            Password = password;
            Id = Guid.NewGuid();
            CorrelationId = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public string Username { get; }

        public string Password { get; }
        public Guid Id { get; }
        
        public Guid CorrelationId { get; }
        public DateTime CreationDate { get; }
    }
}