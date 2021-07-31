using System.Collections.Generic;

namespace InstaCrafter.Classes.Models
{
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
}