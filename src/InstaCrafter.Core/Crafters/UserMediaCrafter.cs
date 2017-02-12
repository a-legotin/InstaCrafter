using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using InstaCrafter.Classes.Database;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Loggers;
using InstagramApi.API;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace InstaCrafter.Core.Crafters
{
    public class UserMediaCrafter : ICrafter
    {
        public UserMediaCrafter(ICraftLogger logger, ICraftJob job, int id, IHubContext progressReporter)
        {
            Logger = logger;
            Id = id;
            ProgressReporter = progressReporter;
            Job = job;
        }

        public void Craft()
        {
            Logger.WriteLog(LogMessageType.Info, $"#{Id}: new job found! Id: {Job}");
            var craftUserMediaJob = Job as CraftMediaJob;
            if (craftUserMediaJob == null)
            {
                Logger.WriteLog(LogMessageType.Error, $"#{Id}: craft job not defined, terminating");
                return;
            }
            Job.Progress.Maximum = 20;
            Job.Progress.Current = 1;
            Job.Progress.Status = CraftJobStatus.Running;
            ProgressReporter.Clients.All.reportJobStarted(Job);
            var instaApi = new InstaApiBuilder().Build();
            var userMedia = instaApi.GetUserPostsByUsername(craftUserMediaJob.UserName, craftUserMediaJob.PagesCount);
            if (userMedia == null || userMedia.Count < 1)
            {
                Logger.WriteLog(LogMessageType.Warning,
                    $"#{Id}: there is no media for user {craftUserMediaJob.UserName}, terminating");
                Job.Progress.Status = CraftJobStatus.Completed;
                return;
            }
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("http://localhost:5000/");

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                foreach (var media in userMedia)
                {
                    Thread.Sleep(200);
                    Job.Progress.Current++;
                    Logger.WriteLog(LogMessageType.Info,
                        $"#{Id}:processing post: {media.Code} for user {craftUserMediaJob.UserName}");
                    ProgressReporter.Clients.All.reportJobProgress(Job);

                    var response = client.GetAsync($"api/post/{media.Code}");
                    if (response.Result.StatusCode != HttpStatusCode.OK)
                        Logger.WriteLog(LogMessageType.Error, $"#{Id}: unable to check post : {media.Code}, skipping");

                    var postJson = response.Result.Content.ReadAsStringAsync().Result;
                    var post = JsonConvert.DeserializeObject<InstaPost>(postJson);

                    var mediaContent = new StringContent(JsonConvert.SerializeObject(media), Encoding.UTF8,
                        "application/json");
                    if (post.Equals(InstaPost.Empty))
                    {
                        Logger.WriteLog(LogMessageType.Info,
                            $"#{Id}: inserting post : {media.Code} for user {craftUserMediaJob.UserName}");

                        client.PostAsync("/api/post", mediaContent);
                    }
                    else
                    {
                        Logger.WriteLog(LogMessageType.Info,
                            $"#{Id}: updating post : {media.Code} for user {craftUserMediaJob.UserName}");

                        client.PutAsync($"api/post/{post.Id}", mediaContent);
                    }
                }
            }

            Job.Progress.Status = CraftJobStatus.Completed;
            Logger.WriteLog(LogMessageType.Info, $"#{Id}: job done");
            ProgressReporter.Clients.All.reportJobProgress(Job);
        }

        public ICraftJob Job { get; set; }
        public ICraftLogger Logger { get; }
        public string Name { get; set; } = "User media crafter";
        public int Id { get; }
        public IHubContext ProgressReporter { get; }
    }
}