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

namespace InstaCrafter.Core
{
    public class InstaPostsCrafter : ICrafter<InstaPostList>
    {
        private readonly string Url;
        private readonly string DataStoreUrl = "http://localhost:5000/api/post";
        private readonly CancellationToken _cancelToken;

        public InstaPostsCrafter(string url, CancellationToken token)
        {
            Url = url;
            _cancelToken = token;
        }
        public void Craft()
        {
            if (string.IsNullOrEmpty(Url)) throw new ArgumentException("url");
            if (_cancelToken == null) throw new ArgumentNullException("cancelToken");
            string mediaUrl = Url + "media/";
            while (!_cancelToken.IsCancellationRequested)
            {
                InstaUser user = GetUser();
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


                var instaresponse = JsonConvert.DeserializeObject<Classes.Wrapper.InstaResponse>(html);
                var converter = ConvertersFabric.GetPostsConverter(instaresponse);
                var posts = converter.Convert();
                foreach (var post in posts)
                {
                    post.UserId = user.Id;
                    var contentBase = new StringContent(JsonConvert.SerializeObject(post), Encoding.UTF8, "application/json");
                    var response = client.GetAsync($"{DataStoreUrl}/{post.Code}");
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

        public InstaUser GetUser()
        {
            var user = new InstaUser();
            string userUrl = Url + "?__a=1";
            string html;
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(userUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            var task = client.GetStreamAsync(userUrl);
            using (var reader = new StreamReader(task.Result))
            {
                html = reader.ReadToEnd();
            }
            var root = Newtonsoft.Json.Linq.JObject.Parse(html);
            var userObject = root["user"];
            var instaresponse = JsonConvert.DeserializeObject<Classes.Wrapper.InstaResponseUser>(userObject.ToString());
            var converter = ConvertersFabric.GetUserConverter(instaresponse);
            return converter.Convert();
        }
    }
}
