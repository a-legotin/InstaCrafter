using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Web.DataAccess;
using InstaCrafter.Web.Mapper;
using InstaCrafter.Web.Models;
using InstaCrafter.Web.Scrapper;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Classes.Models;
using InstaSharper.Logger;
using InstaUser = InstaCrafter.Web.Models.InstaUser;

namespace InstaCrafter.Web
{
    public class TaskRunner : ITaskRunner
    {
        private readonly IInstaApi instaApi;
        private readonly IUnitOfWork uow;
        public TaskRunner(IUnitOfWork uow)
        {
            this.uow = uow;
            instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(new UserSessionData { UserName = "alex_codegarage", Password = Environment.GetEnvironmentVariable("instaapiuserpassword") })
                .SetRequestDelay(TimeSpan.FromSeconds(2))
                .UseLogger(new DebugLogger(LogLevel.All))
                .Build();
        }
        public async Task<bool> Run()
        {
            try
            {
                const string stateFile = "state.bin";
                try
                {
                    if (File.Exists(stateFile))
                    {
                        Stream fs = File.OpenRead(stateFile);
                        fs.Seek(0, SeekOrigin.Begin);
                        instaApi.LoadStateDataFromStream(fs);
                        fs.Dispose();
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                if (!instaApi.IsUserAuthenticated)
                    await instaApi.LoginAsync();

                var state = instaApi.GetStateDataAsStream();
                using (var fileStream = File.Create(stateFile))
                {
                    state.Seek(0, SeekOrigin.Begin);
                    state.CopyTo(fileStream);
                }


                var currentUserResult = await instaApi.GetUserAsync("alexandr_le");
                var currentUser = MapperInternal.Instance.Map<InstaUserShort, InstaUser>(currentUserResult.Value);

                uow.UserRepository.CreateOrUpdate(currentUser);

                var scrapper = new FollowersScrapper(instaApi);
                var followers = await  scrapper.Scrap(currentUser.UserName);
                foreach (var follower in followers)
                {
                    uow.UserRepository.CreateOrUpdate(follower);
                }

                var followingScrapper = new FollowingsScrapper(instaApi);
                var followings = await followingScrapper.Scrap(currentUser.UserName);
                foreach (var following in followings)
                {
                    uow.UserRepository.CreateOrUpdate(following);
                }
                
                var allUsers = new List<InstaUser>{currentUser}.Concat(followings).Concat(followers);
                foreach (var user in allUsers)
                {
                    var mediasRes = await instaApi.GetUserMediaAsync(user.UserName, 5);
                    if(!mediasRes.Succeeded || mediasRes.Value == null)
                        continue;
                    foreach (var media in mediasRes.Value)
                    {
                        var backMedia = MapperInternal.Instance.Map<InstaMedia, InstaMediaPost>(media);
                        backMedia.User = user;
                        user.Medias.Add(backMedia);
                    }

                    var story = await instaApi.GetUserStoryAsync(user.Pk);
                    if (!story.Succeeded && !(story.Value?.Items?.Count > 0))
                        continue;

                    var storyToBackup = MapperInternal.Instance.Map<InstaSharper.Classes.Models.InstaStory, Models.InstaStory>(story.Value);
                    storyToBackup.User = user;
                    foreach (var item in storyToBackup.Items)
                    {
                        item.User = user;
                    }
                    user.Stories.Add(storyToBackup);

                    uow.UserRepository.CreateOrUpdate(user);
                }
                
                uow.SaveChanges();

                return true;
            }
            catch (Exception e)
            {
                Debug.Write(e);
            }
            return false;
        }
    }
}