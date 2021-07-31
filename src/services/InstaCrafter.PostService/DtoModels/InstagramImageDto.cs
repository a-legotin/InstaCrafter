namespace InstaCrafter.PostService.DtoModels
{
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