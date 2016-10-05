using System;
using System.Collections.Generic;
using System.Net.Http;
using InstaCrafter.Models;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace InstaCrafter.Core
{
    public class InstaPostsCrafter : ICrafter<InstaPost>
    {
        public InstaPostsCrafter(string url)
        {
            Url = url;
        }
        public string Url { get; set; }
        public List<InstaPost> Craft()
        {
            var posts = new List<InstaPost>();
            string html;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStreamAsync(Url);
            using (var reader = new StreamReader(task.Result))
            {
                html = reader.ReadToEnd();
            }

            var instaresponse = JsonConvert.DeserializeObject<Classes.Wrapper.InstaResponse>(html);
            var converter = ConvertersFabric.GetPostsConverter(instaresponse);
            return posts;
        }
    }
}
