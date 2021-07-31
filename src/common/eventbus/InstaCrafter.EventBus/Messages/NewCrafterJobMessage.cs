using InstaCrafter.Classes.Models;

namespace InstaCrafter.EventBus.Messages
{
    public class NewCrafterJobMessage : IntegrationMessage
    {
        public NewCrafterJobMessage(JobType jobType, string user)
        {
            User = user;
            JobType = jobType;
        }
        
        public JobType JobType { get; }

        public string User { get; }
    }
}