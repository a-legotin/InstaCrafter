using System;
using System.Collections.Generic;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.PostService.DtoModels
{
    public class InstagramPostDto
    {
        public long Id { get; set; }
        public DateTime TakenAt { get; set; }
        public string Pk { get; set; }

        public string InstaIdentifier { get; set; }

        public DateTime DeviceTimeStamp { get; set; }

        public InstagramMediaType MediaType { get; set; }

        public string Code { get; set; }

        public string ClientCacheKey { get; set; }
        public string FilterType { get; set; }

        public virtual ICollection<InstagramImageDto> Images { get; set; } = new List<InstagramImageDto>();
        public virtual ICollection<InstagramVideoDto> Videos { get; set; } = new List<InstagramVideoDto>();

        public int Width { get; set; }
        public string Height { get; set; }
        
        public string TrackingToken { get; set; }

        public int LikesCount { get; set; }

        public string NextMaxId { get; set; }

        public string CommentsCount { get; set; }

        public bool PhotoOfYou { get; set; }

        public bool HasLiked { get; set; }
        
        public int ViewCount { get; set; }

        public bool HasAudio { get; set; }
        
        public long UserId { get; set; }
    }
    
    public class InstagramVideoDto
    {
        public long Id { get; set; }
        
        public InstagramPostDto Post {get;set;}

        public string Url { get; set; }
        
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public int Type { get; set; }
    }

    public class InstagramImageDto
    {
        public long Id { get; set; }
        
        public InstagramPostDto Post {get;set;}

        public string URI { get; set; }
        
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }

}