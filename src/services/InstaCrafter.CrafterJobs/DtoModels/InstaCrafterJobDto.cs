using System;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.CrafterJobs.DtoModels
{
    public class InstaCrafterJobDto
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public JobType JobType { get; set; }
        public DateTime CratedAt { get; set; }

        public override string ToString()
        {
            return $"Username: {UserName}, type: {JobType}";
        }
    }
}