using System;

namespace InstaCrafter.PostService.DtoModels
{
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
}