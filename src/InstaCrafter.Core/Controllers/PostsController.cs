﻿using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using InstaCrafter.Classes.Database;
using InstaCrafter.Core.Models;
using Newtonsoft.Json;

namespace InstaCrafter.Core.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Posts()
        {
            InstaPostList posts;
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var response = client.GetAsync($"api/post/");
                if (response.Result.StatusCode != HttpStatusCode.OK)
                    throw new Exception("Unable to get posts from data store");

                var postsJson = response.Result.Content.ReadAsStringAsync().Result;
                posts = JsonConvert.DeserializeObject<InstaPostList>(postsJson);
            }
            var model = new Posts {List = posts};
            return View(model);
        }
    }
}