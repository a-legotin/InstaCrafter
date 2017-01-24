using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Web;
using Akka.Actor;
using Consul;
using InstaCrafter.Classes.Database;
using InstaCrafter.Core.Actors;
using InstagramApi.API;
using Newtonsoft.Json;

namespace InstaCrafter.Core
{
    public class InstaWorkerSimple
    {
        public InstaWorkerSimple(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
        public static ActorSystem MyActorSystem;

        public async void Craft()
        {
            //var client = new ConsulClient();
            //var serviceList = client.Catalog.Service("instacrafter.datastore");
            //var serv = serviceList.Result;
            //int retryCount = 0;
            //while (serv.Response.Length < 1 && retryCount < 5)
            //{
            //    Thread.Sleep(2000);
            //    serviceList = client.Catalog.Service("instacrafter.datastore");
            //    serv = serviceList.Result;
            //    retryCount++;
            //}
            //var instaApi = new InstaApiBuilder().Build();
            //var userMedia = instaApi.GetUserPostsByUsername(Username);
            //var httpclient = new HttpClient();
            //var firstOrDefault = serv.Response.FirstOrDefault();
            //if (firstOrDefault == null) return;
            //var mainUri = new Uri($"http://{firstOrDefault.Address}:{firstOrDefault.ServicePort}/api/post/");
            //httpclient.BaseAddress = mainUri;
            //httpclient.DefaultRequestHeaders
            //      .Accept
            //      .Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //var uri = mainUri;

            //foreach (var media in userMedia)
            //{
            //    var post = new InstaPost(0, media.UserId, media.Code) {Link = media.Link, CreatedTime =  media.CreatedTime};
            //    HttpRequestMessage request = null;
            //    HttpMethod method = null;
            //    var getRequset = new HttpRequestMessage(HttpMethod.Get, new Uri(uri + $"/{media.Code}"));
            //    var response = httpclient.SendAsync(getRequset);
            //    if(!response.Result.IsSuccessStatusCode) continue;

            //    var responseString = await response.Result.Content.ReadAsStringAsync();
            //    var postInfo = JsonConvert.DeserializeObject<InstaPost>(responseString);
            //    if (postInfo.Id > 0)
            //    {
            //        post.Id = postInfo.Id;
            //        method = HttpMethod.Put;
            //        uri = new Uri(mainUri, $"{post.Id}");
            //    }
            //    else
            //    {
            //        method = HttpMethod.Post;
            //    }

                
            //    request = new HttpRequestMessage(method, uri)
            //    {
            //        Content =
            //            new StringContent(
            //                JsonConvert.SerializeObject(post),
            //                Encoding.UTF8,
            //                "application/json")
            //    };

            //    var result = httpclient.SendAsync(request);
            //    Debug.WriteLine($"Post {media.Code} processed with result code: {result.Result.StatusCode}");
            //}
            MyActorSystem = ActorSystem.Create("MyActorSystem");
            Props initiatorProps = Props.Create<CraftJobInitiator>();
            var initiatorActor = MyActorSystem.ActorOf(initiatorProps, "jobInitiator");

            initiatorActor.Tell(new CraftJobInitiator.InitialCraftJob("alexandr_le"));

            MyActorSystem.WhenTerminated.Wait();
        }
    }
}