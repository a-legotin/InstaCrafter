using System;
using System.Collections.Generic;

namespace InstaCrafter.Classes.Models
{
    public class InstagramReelFeed
    {
        public long HasBestiesMedia { get; set; }

        public long PrefetchCount { get; set; }

        public bool CanReshare { get; set; }

        public bool CanReply { get; set; }

        public DateTime ExpiringAt { get; set; }

        public List<InstagramStoryItem> Items { get; set; } = new List<InstagramStoryItem>();

        public long Id { get; set; }

        public long LatestReelMedia { get; set; }

        public long Seen { get; set; }

        public InstagramUser? User { get; set; }
    }
}