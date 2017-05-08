using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Database;
using InstaCrafter.Console.Providers;
using InstaSharper.API.Builder;
using InstaSharper.Classes;

namespace InstaCrafter.Console
{
    public class Program
    {
        private readonly IDataAccessProvider<InstaMediaDb> _mediaAccessProvider;
        private readonly IDataAccessProvider<InstaUserDb> _usersAccessProvider;

        public Program(IDataAccessProvider<InstaUserDb> usersAccessProvider,
            IDataAccessProvider<InstaMediaDb> mediaAccessProvider)
        {
            _usersAccessProvider = usersAccessProvider;
            _mediaAccessProvider = mediaAccessProvider;
        }

        public async Task Run(CancellationToken cancelationToken)
        {
            try
            {
                var users = new[] {"alexandr_le", "therock", "ladygaga", "jackblack", "markruffalo"};


                var userCred = new UserSessionData
                {
                    UserName = "alex_codegarage",
                    Password = Environment.GetEnvironmentVariable("instaapiuserpassword")
                };

                var apiInstance = new InstaApiBuilder()
                    .SetUser(userCred)
                    .UseLogger(new Logger())
                    .Build();
                var login = await apiInstance.LoginAsync();

                if (!login.Succeeded)
                {
                    System.Console.WriteLine($"Unable to login: {login.Info.Message}");
                    return;
                }


                foreach (var username in users)
                    try
                    {
                        var user = await apiInstance.GetUserAsync(username);
                        var userDb = Mapper.Map<InstaUserDb>(user.Value);
                        var userPosts = await apiInstance.GetUserMediaAsync(username, 1);

                        _usersAccessProvider.Add(userDb);

                        foreach (var userPost in userPosts.Value)
                        {
                            var postDb = Mapper.Map<InstaMediaDb>(userPost);
                            _mediaAccessProvider.Add(postDb);
                        }
                    }
                    catch (Exception e)
                    {
                        System.Console.WriteLine(e.Message);
                    }


                System.Console.WriteLine("Done...");
                System.Console.ReadKey();
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
        }
    }
}