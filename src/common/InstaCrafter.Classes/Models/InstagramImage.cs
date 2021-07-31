namespace InstaCrafter.Classes.Models
{
    public class InstagramImage
    {
        public string? URI { get; set; }
        
        public byte[]? ImageBytes { get; set; }

        public string? Path { get; set; }
        
        public int Width { get; set; }

        public int Height { get; set; }
    }
}