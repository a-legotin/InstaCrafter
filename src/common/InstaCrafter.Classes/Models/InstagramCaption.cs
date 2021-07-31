using System;

namespace InstaCrafter.Classes.Models
{
    public class InstagramCaption
    {
        public InstagramCaption(long userId, DateTime createdAtUtc, DateTime createdAt, InstagramUser user, string text,
            string mediaId, string pk)
        {
            UserId = userId;
            CreatedAtUtc = createdAtUtc;
            CreatedAt = createdAt;
            User = user;
            Text = text;
            MediaId = mediaId;
            Pk = pk;
        }

        public InstagramCaption()
        {
        }

        public long? UserId { get; set; }
        public DateTime? CreatedAtUtc { get; set; }

        public DateTime? CreatedAt { get; set; }

        public InstagramUser? User { get; set; }

        public string? Text { get; set; }

        public string? MediaId { get; set; }

        public string? Pk { get; set; }
    }
}