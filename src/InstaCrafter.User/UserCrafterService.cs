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

        public UserCrafterService(IEventBus eventBus, IUserDataProvider userProvider,
            ILogger<UserCrafterService> logger)
        {
            _eventBus = eventBus;
            _logger = logger;
            _userProvider = userProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(10));
            _logger.LogDebug("Executing users loading task");
            try
            {
                var user = await _userProvider.GetUser("alexandr_le");
                _eventBus.Publish(new UserLoadedMessage(user));

//                var userFollowers = await _userProvider.GetUserFollowers(user.UserName);
//                foreach (var follower in userFollowers.Randomize())
//                {
//                    _eventBus.Publish(new UserLoadedMessage(follower));
//                }

                var userFollowings = await _userProvider.GetUserFollowings(user.UserName);
                foreach (var following in userFollowings.Randomize())
                {
                    _logger.LogDebug($"Passing {following.UserName} to the bus");
                    _eventBus.Publish(new UserLoadedMessage(following));
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e, $"Error in executing task");
            }
           
            _logger.LogDebug("Task completed");
        }
    }
}