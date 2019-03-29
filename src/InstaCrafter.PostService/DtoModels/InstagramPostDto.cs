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
        public InstagramCaptionDto Caption { get; set; }

        public virtual ICollection<InstagramCarouselItemDto> Carousel { get; set; }
        public InstagramLocationDto Location { get; set; }
    }
    
    public class InstagramCarouselItemDto
    {
        public long Id { get; set; }
        
        public InstagramPostDto Post {get;set;}

        public string InstaIdentifier { get; set; }

        public InstagramMediaType MediaType { get; set; }

        public virtual ICollection<InstagramImageDto> Images { get; set; } = new List<InstagramImageDto>();

        public virtual ICollection<InstagramVideoDto> Videos { get; set; } = new List<InstagramVideoDto>();

        public int Width { get; set; }

        public int Height { get; set; }

        public string Pk { get; set; }

        public string CarouselParentId { get; set; }
    }

    public class InstagramCaptionDto
    {
        public long Id { get; set; }
        
        public long UserId { get; set; }
        public DateTime CreatedAtUtc { get; set; }

        public DateTime CreatedAt { get; set; }
        
        public string Text { get; set; }

        public string MediaId { get; set; }

        public string Pk { get; set; }
    }
    
    public class InstagramLocationDto
    {
        public long Id { get; set; }
        
        public virtual ICollection<InstagramPostDto> Posts { get; set; }

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

    
    public class InstagramVideoDto
    {
        public long Id { get; set; }
        
        public InstagramCarouselItemDto Carousel {get;set;}
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

        public InstagramCarouselItemDto Carousel {get;set;}

        public InstagramPostDto Post {get;set;}

        public string URI { get; set; }
        
        public string Path { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }
    }

}