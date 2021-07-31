using System;

namespace InstaCrafter.Classes.Models
{
    public class InstagramCaption
    {
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }

        public InstagramUser User { get; set; }

        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }
}