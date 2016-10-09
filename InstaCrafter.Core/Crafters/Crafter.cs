using System;
using System.Collections.Generic;
using System.Net.Http;
using InstaCrafter.Models;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using InstaCrafter.Classes.Database;
using System.Text;

namespace InstaCrafter.Core
{
    public class InstaPostsCrafter : ICrafter<InstaPostList>
    {
        public InstaPostsCrafter(string url)
        {
            Url = url;
        }
        public string Url { get; set; }
        public InstaPostList Craft()
        {
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
            var posts = converter.Convert();
            foreach (var post in posts)
            {
                client.PostAsync("http://localhost:5000/api/posts", new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json"));
            }
            return new InstaPostList();
        }

        public InstaUser GetUser()
        {
            var user = new InstaUser();
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

            var instaresponse = JsonConvert.DeserializeObject<Classes.Wrapper.InstaResponseUser>(html);
            var converter = ConvertersFabric.GetUserConverter(instaresponse);
            return converter.Convert();
        }
    }
}
