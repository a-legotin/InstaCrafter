using System;
using Newtonsoft.Json;

namespace InstaCrafter.EventBus.Messages
{
    public class IntegrationMessage
    {
        public IntegrationMessage()
        {
            Guid = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationMessage(Guid guid, DateTime createDate)
        {
            Guid = guid;
            CreationDate = createDate;
        }

        [JsonProperty] public Guid Guid { get; private set; }

        [JsonProperty] public DateTime CreationDate { get; private set; }
    }
}