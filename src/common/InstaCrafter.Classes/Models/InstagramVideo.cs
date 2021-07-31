namespace InstaCrafter.Classes.Models
{
    public class InstagramVideo
    {
        public string? Url { get; set; }

        public byte[]? VideoBytes { get; set; }

        public string? Path { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public int? Type { get; set; }
    }
}