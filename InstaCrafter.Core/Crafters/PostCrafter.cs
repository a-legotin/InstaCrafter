using System;
using System.Collections.Generic;
using System.Net.Http;
using InstaCrafter.Models;
using System.IO;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using InstaCrafter.Classes.Database;
using System.Text;
using System.Threading;
using InstaCrafter.Classes.Wrapper;

namespace InstaCrafter.Core
{
    public class PostCrafter : ICrafter<InstaPostList>
    {
        public InstaPostList CraftItem { get; set; }
        protected string UserName { get; }

        public string DataStoreUrl { get; }

        public CancellationToken CancelToken { get; }

        public PostCrafter(string url, string dataStoreUrl, CancellationToken token)
        {
            UserName = url;
            DataStoreUrl = dataStoreUrl;
            CancelToken = token;
        }
        public void Craft()
        {
            if (string.IsNullOrEmpty(UserName)) throw new ArgumentException("UserName");
            if (CancelToken == null) throw new ArgumentNullException("cancelToken");
            while (!CancelToken.IsCancellationRequested)
            {
                InstaUser user = GetUser(UserName);
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var posts = GetUserPosts(UserName);
                foreach (var post in posts)
                {
                    post.UserId = user.Id;
                    var contentBase = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                    var response = client.GetAsync($"{DataStoreUrl}/post{post.Code}");
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var responseJson = response.Result.Content.ReadAsStringAsync();
                        var oldPost = JsonConvert.DeserializeObject<InstaPost>(responseJson.Result);

                        if (oldPost.Equals(InstaPost.Empty))
                            client.PostAsync(DataStoreUrl, contentBase);
                        else
                            client.PutAsync(DataStoreUrl, contentBase);
                    }
                }
                Thread.Sleep(10000);
            }

        }

        public InstaUser GetUser(string username)
        {
            var user = new InstaUser();
            string userUrl = "https://www.instagram.com/" + UserName + "/?__a=1";
            string html;
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStreamAsync(userUrl);
            using (var reader = new StreamReader(task.Result))
            {
                html = reader.ReadToEnd();
            }
            var root = Newtonsoft.Json.Linq.JObject.Parse(html);
            var userObject = root["user"];
            var instaresponse = JsonConvert.DeserializeObject<InstaResponseUser>(userObject.ToString());
            var converter = ConvertersFabric.GetUserConverter(instaresponse);
            return converter.Convert();
        }
        public InstaPostList GetUserPosts(string username)
        {
            var posts = new InstaPostList();
            string mediaUrl = "https://www.instagram.com/" + username + "/media/";
            string html;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(mediaUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(task.Result))
            {
                html = reader.ReadToEnd();
            }


            var instaresponse = JsonConvert.DeserializeObject<InstaResponse>(html);
            var converter = ConvertersFabric.GetPostsConverter(instaresponse);
            posts.AddRange(converter.Convert());
            while (instaresponse.MoreAvailable)
            {
                instaresponse = GetUserPostsResponseWithMaxId(username, instaresponse.GetLastId());
                converter = ConvertersFabric.GetPostsConverter(instaresponse);
                posts.AddRange(converter.Convert());
            }
            return posts;
        }

        public InstaResponse GetUserPostsResponseWithMaxId(string username, string Id)
        {
            var posts = new InstaPostList();
            string mediaUrl = "https://www.instagram.com/" + username + "/media/?max_id=" + Id;
            string html;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(mediaUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStreamAsync(mediaUrl);
            using (var reader = new StreamReader(task.Result))
            {
                html = reader.ReadToEnd();
            }
            return JsonConvert.DeserializeObject<InstaResponse>(html);
        }
    }
}
