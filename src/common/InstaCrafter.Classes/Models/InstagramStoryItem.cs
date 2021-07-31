using System;
using System.Collections.Generic;

namespace InstaCrafter.Classes.Models
{
    public class InstagramStoryItem
    {
        public bool HasLiked { get; set; }

        public string Code { get; set; }

        public bool CanReshare { get; set; }

        public string AdAction { get; set; }

        public bool CanViewerSave { get; set; }

        public long CaptionPosition { get; set; }

        public bool CaptionIsEdited { get; set; }

        public DateTime DeviceTimestamp { get; set; }

        public bool CommentLikesEnabled { get; set; }

        public long CommentCount { get; set; }

        public bool CommentThreadingEnabled { get; set; }

        public long FilterType { get; set; }

        public DateTime ExpiringAt { get; set; }

        public bool HasAudio { get; set; }

        public string LinkText { get; set; }

        public long Pk { get; set; }

        public string Id { get; set; }

        public bool HasMoreComments { get; set; }

        public List<InstagramImage> ImageList { get; set; } = new List<InstagramImage>();

        public long LikeCount { get; set; }

        public bool IsReelMedia { get; set; }

        public string OrganicTrackingToken { get; set; }

        public long MediaType { get; set; }

        public long MaxNumVisiblePreviewComments { get; set; }

        public long NumberOfQualities { get; set; }

        public long OriginalWidth { get; set; }

        public long OriginalHeight { get; set; }

        public bool PhotoOfYou { get; set; }

        public DateTime TakenAt { get; set; }

        public string VideoDashManifest { get; set; }

        public bool SupportsReelReactions { get; set; }

        public InstagramUser User { get; set; }

        public double VideoDuration { get; set; }

        public List<InstagramVideo> VideoList { get; set; } = new List<InstagramVideo>();
    }
}