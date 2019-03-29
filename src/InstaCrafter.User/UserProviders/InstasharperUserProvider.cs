using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace InstaCrafter.UserCrafter.UserProviders
{
    public class InstasharperUserProvider : IUserDataProvider
    {
        private readonly IOptions<InstaSharperConfig> _appConfig;
        private readonly ILogger _logger;
        private readonly IInstaApi _instaApi;

        public InstasharperUserProvider(IOptions<InstaSharperConfig> appConfig,
            ILogger<InstasharperUserProvider> logger)
        {
            _appConfig = appConfig;
            _logger = logger;
            var userSession = new UserSessionData
            {
                UserName = _appConfig.Value.Username,
                Password = _appConfig.Value.Password
            };

            _instaApi = InstaApiBuilder.CreateBuilder()
                .SetUser(userSession)
                .UseLogger(new DebugLogger(InstaSharper.Logger.LogLevel.Exceptions))
                .SetRequestDelay(RequestDelay.FromSeconds(5, 20))
                .Build();

            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    _logger.LogDebug("Loading state from file");
                    using (var fs = File.OpenRead(stateFile))
                    {
                        _instaApi.LoadStateDataFromStream(fs);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }

            if (!_instaApi.IsUserAuthenticated)
            {
                var logInResult = _instaApi.LoginAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                if (!logInResult.Succeeded)
                {
                    _logger.LogDebug($"Unable to login: {logInResult.Info.Message}");
                    return;
                }
            }

            var state = _instaApi.GetStateDataAsStream();
            using (var fileStream = File.Create(stateFile))
            {
                state.Seek(0, SeekOrigin.Begin);
                state.CopyTo(fileStream);
                _logger.LogDebug($"Instasharper state saved to: {stateFile}");
            }

            _logger.LogDebug(
                $"Instasharper library initialized. User '{_appConfig.Value.Username}' authenticated: {_instaApi.IsUserAuthenticated}");
        }

        public async Task<InstagramUser> GetUser(string username)
        {
            var user = await _instaApi.GetUserAsync(username);
            if (user.Succeeded)
                return Mapper.Map<InstagramUser>(user.Value);
            _logger.LogError($"Unable to load user '{username}': {user.Info.Message}");
            return InstagramUser.Empty;
        }

        public async Task<IEnumerable<InstagramUser>> GetUserFollowings(string username)
        {
            _logger.LogDebug($"Loading followings for user '{username}'");
            var followingResult =
                await _instaApi.GetUserFollowingAsync(username, PaginationParameters.Empty);
            if (followingResult.Succeeded)
            {
                _logger.LogDebug($"Loaded {followingResult.Value.Count} followers for user '{username}'");
                return followingResult.Value.Select(Mapper.Map<InstagramUser>);
            }

            _logger.LogError($"Unable to load user '{username}' followings: {followingResult.Info.Message}");
            return new List<InstagramUser>();
        }

        public async Task<IEnumerable<InstagramUser>> GetUserFollowers(string username)
        {
            _logger.LogDebug($"Loading followers for user '{username}'");
            var followersResult =
                await _instaApi.GetUserFollowersAsync(username, PaginationParameters.MaxPagesToLoad(1));
            if (followersResult.Succeeded)
            {
                _logger.LogDebug($"Loaded {followersResult.Value.Count} followers for user '{username}'");
                return followersResult.Value.Select(Mapper.Map<InstagramUser>);
            }

            _logger.LogError($"Unable to load user '{username}' followers: {followersResult.Info.Message}");
            return new List<InstagramUser>();
        }
    }
}