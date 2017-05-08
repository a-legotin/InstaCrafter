using System;

namespace InstaCrafter.Classes.Database
{
    public class InstaMediaDb
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public virtual InstaUserDb User { get; set; }

        public string Link { get; set; }
        public long Pk { get; set; }
        public string ImageLink { get; set; }

        public DateTime CreatedTime { get; set; }
        public DateTime UpdatedTimestamp { get; set; }

        public static InstaMediaDb Empty => new InstaMediaDb();
    }
}