using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace InstaCrafter.Classes.Wrapper
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
