using System;
using InstaCrafter.Classes.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace InstaCrafter.Classes.Wrapper
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
