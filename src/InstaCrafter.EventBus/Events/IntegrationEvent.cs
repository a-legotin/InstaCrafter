using System;
using Newtonsoft.Json;

namespace InstaCrafter.EventBus.Events
{
    public class IntegrationEvent
    {
        public IntegrationEvent()
        {
            Guid = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        [JsonConstructor]
        public IntegrationEvent(Guid guid, DateTime createDate)
        {
            Guid = guid;
            CreationDate = createDate;
        }

        [JsonProperty]
        public Guid Guid { get; private set; }

        [JsonProperty]
        public DateTime CreationDate { get; private set; }
    }
}
