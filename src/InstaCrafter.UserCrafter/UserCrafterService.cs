using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes.Models;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.Extensions;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using InstaCrafter.UserCrafter.UserProviders;
using InstaSharper.API;
using InstaSharper.API.Builder;
using InstaSharper.Classes;
using InstaSharper.Logger;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using LogLevel = InstaSharper.Logger.LogLevel;

namespace InstaCrafter.UserCrafter
{
    public class UserCrafterService : BackgroundService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger _logger;
        private readonly IUserDataProvider _userProvider;

        public UserCrafterService(IEventBus eventBus, IUserDataProvider userProvider, ILogger<UserCrafterService> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
            _userProvider = userProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogDebug("Executing users loading task");
            var user = await _userProvider.GetUser("alexandr_le");
            _eventBus.Publish(new UserLoadedEvent(user));
            
            var userFollowers = await _userProvider.GetUserFollowers(user.UserName);
            foreach (var follower in userFollowers.Randomize())
            {
                _eventBus.Publish(new UserLoadedEvent(follower));
            }
            
            var userFollowings = await _userProvider.GetUserFollowings(user.UserName);
            foreach (var following in userFollowings.Randomize())
            {
                _eventBus.Publish(new UserLoadedEvent(following));
            }
            _logger.LogDebug("Task completed");
        }
    }
}