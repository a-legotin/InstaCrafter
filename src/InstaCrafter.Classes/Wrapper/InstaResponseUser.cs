using Newtonsoft.Json;

namespace InstaCrafter.DataAccess.Wrapper
{
    public class InstaResponseUser
    {
        [JsonProperty("username")]
        public string UserName { get; set; }

        [JsonProperty("profile_pic_url")]
        public string ProfilePicture { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("full_name")]
        public string FullName { get; set; }
    }
}