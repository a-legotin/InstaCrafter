using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace InstaCrafter.UserCrafter
{
    public class UserCrafterService : BackgroundService
    {
        private readonly IOptions<InstaSharperConfig> _appConfig;
        private readonly IEventBus _eventBus;
        private readonly IInstaApi _instaApi;

        public UserCrafterService(IOptions<InstaSharperConfig> appConfig, IEventBus eventBus)
        {
            _appConfig = appConfig;
            _eventBus = eventBus;
            var userSession = new UserSessionData
            {
                UserName = _appConfig.Value.Username,
                Password = _appConfig.Value.Password
            };


            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(LogLevel.Exceptions))
                .SetRequestDelay(RequestDelay.FromSeconds(1, 3))
                .Build();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    Console.WriteLine("Loading state from file");
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            if (!_instaApi.IsUserAuthenticated)
            {
                var logInResult = await _instaApi.LoginAsync();
                if (!logInResult.Succeeded)
                {
                    Console.WriteLine($"Unable to login: {logInResult.Info.Message}");
                    return;
                }
            }

            var state = _instaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
            }

            var user = await _instaApi.GetUserAsync("alexandr_le");
            _eventBus.Publish(new UserLoadedEvent(user.Value.Pk));
            //var following = await _instaApi.GetUserFollowingAsync(user.Value.UserName, PaginationParameters.Empty);
        }
    }
}