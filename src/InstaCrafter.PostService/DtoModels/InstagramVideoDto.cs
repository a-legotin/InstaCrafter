namespace InstaCrafter.PostService.DtoModels
{
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
}