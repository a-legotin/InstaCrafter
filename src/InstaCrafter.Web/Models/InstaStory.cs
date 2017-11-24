using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InstaCrafter.Web.Models
{
    public class InstaStory
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long InternalStoryId { get; set; }

        public bool CanReply { get; set; }

        public DateTime? ExpiringAt { get; set; }

        public InstaUser User { get; set; }

        public string SourceToken { get; set; }

        public DateTime? Seen { get; set; }

        public string LatestReelMedia { get; set; }

        public string Id { get; set; }

        public int RankedPosition { get; set; }

        public bool Muted { get; set; }

        public int SeenRankedPosition { get; set; }

        public int PrefetchCount { get; set; }

        public string SocialContext { get; set; }

        public virtual ICollection<InstaMediaPost> Items { get; set; }

    }
}