using System.Collections.Generic;
using InstaCrafter.Classes.Models;

namespace InstaCrafter.PostService.DtoModels
{
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
}