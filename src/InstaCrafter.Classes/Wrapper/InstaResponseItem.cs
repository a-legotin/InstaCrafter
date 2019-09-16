using System;
using InstaCrafter.DataAccess.Helpers;
using Newtonsoft.Json;

namespace InstaCrafter.DataAccess.Wrapper
{
    public class InstaResponseItem
    {
        public string Code { get; set; }
        public InstagramLocation Location { get; set; }

        public string Link { get; set; }

        public InstagramPostType Type { get; set; }

        [JsonProperty("created_time")]
        public string CreatedTime { get; set; }

        [JsonProperty("can_view_comments")]
        public bool CanViewComment { get; set; }

        public string Id { get; set; }

        public DateTime CreatedTimeConverted => DateTimeHelper.UnixTimestampToDateTime(double.Parse(CreatedTime));
    }

    public class InstagramLocation
    {
        public string Name { get; set; }
    }

    public enum InstagramPostType

    {
        Image = 0,
        Video = 1
    }
}