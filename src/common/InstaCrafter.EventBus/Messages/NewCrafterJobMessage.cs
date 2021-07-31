using System;
using CozyBus.Core.Messages;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class NewCrafterJobMessage : IBusMessage
    {
        public NewCrafterJobMessage(JobType jobType, string user)
        {
            User = user;
            JobType = jobType;
        }

        public JobType JobType { get; }

        public string User { get; }
        public Guid Id { get; }
        public DateTime CreationDate { get; }
    }
}