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
}