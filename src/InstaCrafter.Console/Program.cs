using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstagramApi.API;
using InstagramApi.Classes;
using Newtonsoft.Json;

namespace InstaCrafter.Console
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var runningTask = new Task(() => MainAsync(args));
                runningTask.RunSynchronously(TaskScheduler.Current);
                System.Console.ReadKey();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
        static async void MainAsync(string[] args)
        {
            var apiInstance = new InstaApiBuilder()
                                .SetUserName(@"alexandr_le")
                                .UseLogger(new Logger())
                                .Build();
            var user = apiInstance.GetCurrentUser();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");
                client.Timeout = TimeSpan.FromSeconds(30);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));


                    var response = client.GetAsync($"api/user/{user.UserName}");

                    var userJson = response.Result.Content.ReadAsStringAsync().Result;
                    var userDb = JsonConvert.DeserializeObject<InstaUserDb>(userJson);

                    var userContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8,
                        "application/json");
                    if (string.IsNullOrEmpty(userDb?.UserName))
                    {
                        await client.PostAsync("/api/user", userContent);
                    }
                    else
                    {
                        await client.PutAsync($"api/user/{userDb.Id}", userContent);
                    }
                
            }
            //var userMedia = apiInstance.GetUserPostsByUsername(@"alexandr_le", 2);
            //if (userMedia == null || userMedia.Count < 1)
            //{

            //    return;
            //}
            //using (var client = new HttpClient())
            //{
            //    client.BaseAddress = new Uri("http://localhost:5000/");
            //    client.Timeout = TimeSpan.FromSeconds(30);
            //    client.DefaultRequestHeaders.Accept.Clear();
            //    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //    foreach (var media in userMedia)
            //    {
            //        Thread.Sleep(2000);


            //        var response = client.GetAsync($"api/post/{media.Code}");
            //        if (response.Result.StatusCode != HttpStatusCode.OK)
            //        {
            //            continue;

            //        }

            //        var postJson = response.Result.Content.ReadAsStringAsync().Result;
            //        var post = JsonConvert.DeserializeObject<InstaPost>(postJson);

            //        var mediaContent = new StringContent(JsonConvert.SerializeObject(media), Encoding.UTF8,
            //            "application/json");
            //        if (post.Equals(InstaPost.Empty))
            //        {
            //            client.PostAsync("/api/post", mediaContent);
            //        }
            //        else
            //        {
            //            //client.PutAsync($"api/post/{post.Id}", mediaContent);
            //        }
            //    }
            //}
        }
    }
}
