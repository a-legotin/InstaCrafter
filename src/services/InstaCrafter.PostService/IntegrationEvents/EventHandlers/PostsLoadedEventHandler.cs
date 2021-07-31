using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.EventBus.Abstractions;
using InstaCrafter.PostService.DataProvider;
using InstaCrafter.PostService.DtoModels;
using InstaCrafter.UserCrafter.IntegrationEvents.Events;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.PostService.IntegrationEvents.EventHandlers
{
    public class PostsLoadedEventHandler : IIntegrationEventHandler<PostsLoadedEvent>
    {
        private readonly IDataAccessProvider<InstagramPostDto> _repo;
        private readonly ILogger<PostsLoadedEventHandler> _logger;

        public PostsLoadedEventHandler(IDataAccessProvider<InstagramPostDto> repo, ILogger<PostsLoadedEventHandler> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        public async Task Handle(PostsLoadedEvent postsLoadedEvent)
        {
            _logger.LogDebug($"Got an event! User: '{postsLoadedEvent.UserName}', {postsLoadedEvent.Posts.Count()} posts");
            try
            {
                await Task.Run(() =>
                {
                    foreach (var instagramPost in postsLoadedEvent.Posts)
                    {
                        var dto = Mapper.Map<InstagramPostDto>(instagramPost);
                        if (_repo.Exist(dto))
                        {
                            var existingPost = _repo.Get(instagramPost.Code);
                            _repo.Update(existingPost.Id, dto);
                        }
                        else
                        {
                            _repo.Add(dto);
                        }
                    }
                });

            }
            catch (Exception e)
            {
                _logger.LogCritical(e, "Unable to process event {Guid}", postsLoadedEvent.Guid);
            }
        }
    }
}