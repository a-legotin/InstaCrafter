using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Classes;
using InstaCrafter.Classes.Models;
using InstaCrafter.Extensions;
using InstaCrafter.Media.Classes;
using InstaSharper.Abstractions.API;
using InstaSharper.Builder;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace InstaCrafter.Media.MediaProviders
{
    public class InstasharperMediaProvider : IMediaDataProvider
    {
        private readonly IOptions<InstaSharperConfig> _appConfig;
        private readonly IImageLoader _imageLoader;
        private readonly IInstaApi _instaApi;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public InstasharperMediaProvider(IOptions<InstaSharperConfig> appConfig,
            IMapper mapper,
            ILogger<InstasharperMediaProvider> logger,
            IImageLoader imageLoader)
        {
            _appConfig = appConfig;
            _mapper = mapper;
            _logger = logger;
            _imageLoader = imageLoader;
            var credentials = new UserCredentials(_appConfig.Value.Username, _appConfig.Value.Password);

            _instaApi = Builder.Create()
                .WithUserCredentials(credentials)
                .Build();

            const string stateFile = "state.bin";
            try
            {
                if (File.Exists(stateFile))
                {
                    _logger.LogDebug("Loading state from file");
                    _instaApi.User.LoadStateDataFromBytes(File.ReadAllBytes(stateFile));
                }
            }
            catch (Exception e)
            {
                _logger.LogCritical(e.Message);
            }

            if (!_instaApi.User.IsAuthenticated)
            {
                _instaApi.User
                    .LoginAsync()
                    .ConfigureAwait(false)
                    .GetAwaiter()
                    .GetResult()
                    .Match(user => { },
                        fail =>
                        {
                            _logger.LogDebug($"Unable to login: {fail.Message}");
                            throw new Exception("Unable to log in to Instagram");
                        });
            }

            var state = _instaApi.User.GetUserSessionAsByteArray();
            if (state != null)
            {
                File.WriteAllBytes(stateFile, state);
                _logger.LogDebug($"Instasharper state saved to: {stateFile}");
            }

            _logger.LogDebug(
                $"Instasharper library initialized. User '{_appConfig.Value.Username}' authenticated: {_instaApi.User.IsAuthenticated}");
        }

        public async Task<IEnumerable<InstagramPost>> GetUserPosts(string username)
        {
            // _logger.LogDebug($"Loading posts for user '{username}'");
            // var getMediaResult =
            //     await _instaApi.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(3));
            // if (getMediaResult.Succeeded)
            // {
            //     var posts = new List<InstagramPost>();
            //     foreach (var media in getMediaResult.Value)
            //     {
            //         var post = _mapper.Map<InstagramPost>(media);
            //         post.Carousel = _mapper.Map<List<InstagramCarouselItem>>(media.Carousel as List<InstaCarouselItem>);
            //         if (post.Carousel != null)
            //         {
            //             foreach (var carouselItem in post.Carousel)
            //             {
            //                 foreach (var carouselItemImage in carouselItem.Images)
            //                 {
            //                     await ProcessImage(username, carouselItemImage, Path.Combine(post.Code, carouselItem.Pk));
            //                 }
            //                 foreach (var carouselItemVideo in carouselItem.Videos)
            //                 {
            //                     await ProcessVideo(username, carouselItemVideo, Path.Combine(post.Code, carouselItem.Pk));
            //                 }
            //             }
            //         }
            //         else
            //         {
            //             foreach (var instagramImage in post.Images)
            //             {
            //                 await ProcessImage(username, instagramImage, post.Code);
            //             }
            //
            //             foreach (var instagramVideo in post.Videos)
            //             {
            //                 await ProcessVideo(username, instagramVideo, post.Code);
            //             }
            //         }
            //         posts.Add(post);
            //     }
            //
            //     _logger.LogDebug($"Loaded {getMediaResult.Value.Count} posts for user '{username}'");
            //     return posts;
            // }

            //_logger.LogError($"Unable to load user '{username}' all posts: {getMediaResult.Info.Message}");
            await Task.CompletedTask;
            return new List<InstagramPost>();
        }

        public async Task<InstagramReelFeed> GetUserStory(string username)
        {
            _logger.LogDebug($"Loading story for user '{username}'");
            return (await _instaApi.User.GetUserAsync(username))
                .Match(user => { return new InstagramReelFeed(); },
                    fail =>
                    {
                        _logger.LogError($"Unable to load user's '{username}' all posts: {fail.Message}");
                        return new InstagramReelFeed();
                    });


            // var getStoryResult = await _instaApi.GetUserStoryFeedAsync(user);
            // if (getStoryResult.Succeeded)
            // {
            //     _logger.LogDebug($"Loaded {getStoryResult.Value.Items.Count} stories for user '{username}'");
            //     return _mapper.Map<InstagramReelFeed>(getStoryResult.Value);
            // }
            //
            // if (!user.IsRight)
            // {
            //     
            // }
            //
            //
            //
            // _logger.LogError($"Unable to load user '{username}' all posts: {getStoryResult.Info.Message}");
            // return new InstagramReelFeed();
        }

        private async Task ProcessVideo(string username, InstagramVideo instagramVideo, string postSubPath)
        {
            var videoStream =
                await _imageLoader.LoadVideoAsStream(new Uri(instagramVideo.Url)) as MemoryStream;

            var filename = string.Concat(instagramVideo.Width.ToString(),
                "x",
                instagramVideo.Height.ToString(),
                ".mp4");
            var relativePath = Path.Combine(username, postSubPath);
            var relativeFileName = Path.Combine(relativePath, filename);
            var localPath = Path.Combine(_appConfig.Value.FileStoragePath, relativePath);

            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);
            var path = Path.Combine(localPath, filename);
            if (!File.Exists(path))
            {
                File.WriteAllBytes(path, videoStream.ToArray());
            }

            instagramVideo.Path = relativeFileName;
        }

        private async Task ProcessImage(string username, InstagramImage instagramImage, string postSubPath)
        {
            var image = await _imageLoader.LoadImage(new Uri(instagramImage.URI));
            var filename = string.Concat(instagramImage.Width.ToString(),
                "x",
                instagramImage.Height.ToString(),
                ".jpg");
            var relativePath = Path.Combine(username, postSubPath);
            var relativeFileName = Path.Combine(relativePath, filename);
            var localPath = Path.Combine(_appConfig.Value.FileStoragePath, relativePath);

            if (!Directory.Exists(localPath))
                Directory.CreateDirectory(localPath);
            var path = Path.Combine(localPath, filename);
            if (!File.Exists(path))
                await image.SaveAsJpegAsync(path);
            instagramImage.Path = relativeFileName;
            instagramImage.ImageBytes = await File.ReadAllBytesAsync(path);
        }
    }
}