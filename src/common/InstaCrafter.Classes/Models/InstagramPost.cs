using System;
using System.Collections.Generic;

namespace InstaCrafter.Classes.Models
{
    public class InstagramPost
    {
        public InstagramPost()
        {
        }

        public DateTime TakenAt { get; set; }
        public string Pk { get; set; }

        public string InstaIdentifier { get; set; }

        public DateTime DeviceTimeStamp { get; set; }

        public InstagramMediaType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }

        public List<InstagramImage> Images { get; set; } = new List<InstagramImage>();
        public List<InstagramVideo> Videos { get; set; } = new List<InstagramVideo>();

        public int Width { get; set; }
        public string Height { get; set; }

        public InstagramUser User { get; set; }

        public string TrackingToken { get; set; }

        public int LikesCount { get; set; }

        public string NextMaxId { get; set; }

        public string CommentsCount { get; set; }

        public bool PhotoOfYou { get; set; }

        public bool HasLiked { get; set; }

        public List<InstagramUser> Likers { get; set; }

        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }
        
        public InstagramCaption Caption { get; set; }

        public List<InstagramCarouselItem> Carousel { get; set; }
        public InstagramLocation Location { get; set; }
    }
    
    public class InstagramCarouselItem
    {
        public string InstaIdentifier { get; set; }

        public InstagramMediaType MediaType { get; set; }

        public List<InstagramImage> Images { get; set; } = new List<InstagramImage>();

        public List<InstagramVideo> Videos { get; set; } = new List<InstagramVideo>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }
    }
    
    public class InstagramCarousel : List<InstagramCarouselItem>
    { }
    
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
    
    public class InstagramLocation
    {
        public long FacebookPlacesId { get; set; }

        public string City { get; set; }

        public long Pk { get; set; }

        public string ShortName { get; set; }
        
        public string ExternalSource { get; set; }

        public string ExternalId { get; set; }

        public string Address { get; set; }

        public double Lng { get; set; }

        public double Lat { get; set; }

        public string Name { get; set; }
    }

    public class InstagramVideo
    {
        public string Url { get; set; }
        
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Type { get; set; }
    }

    public class InstagramImage
    {
        public string URI { get; set; }
        
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }


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

        public InstagramUser User { get; set; }
    }

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

    public enum InstagramMediaType
    {
        Image = 1,
        Video = 2,
        Carousel = 8
    }
}