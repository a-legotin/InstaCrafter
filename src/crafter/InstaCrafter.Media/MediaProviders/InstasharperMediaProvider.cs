// using System;
// using System.Collections.Generic;
// using System.Drawing.Imaging;
// using System.IO;
// using System.Threading.Tasks;
// using AutoMapper;
// using InstaCrafter.Classes;
// using InstaCrafter.Classes.Models;
// using InstaCrafter.Extensions;
// using InstaSharper.Abstractions.API;
// using InstaSharper.Builder;
// using Microsoft.Extensions.Logging;
// using Microsoft.Extensions.Options;
// using PaginationParameters = InstaSharper.Abstractions.Models.PaginationParameters;
//
// namespace InstaCrafter.Media.MediaProviders
// {
//     public class InstasharperMediaProvider : IMediaDataProvider
//     {
//         private readonly IOptions<InstaSharperConfig> _appConfig;
//         private readonly IMapper _mapper;
//         private readonly ILogger _logger;
//         private readonly IImageLoader _imageLoader;
//         private readonly IInstaApi _instaApi;
//
//         public InstasharperMediaProvider(IOptions<InstaSharperConfig> appConfig,
//             IMapper mapper,
//             ILogger<InstasharperMediaProvider> logger,
//             IImageLoader imageLoader)
//         {
//             _appConfig = appConfig;
//             _mapper = mapper;
//             _logger = logger;
//             _imageLoader = imageLoader;
//             var userSession = new UserSessionData
//             {
//                 UserName = _appConfig.Value.Username,
//                 Password = _appConfig.Value.Password
//             };
//
//             _instaApi = Builder.Create()
//                 .Build();
//
//             const string stateFile = "state.bin";
//             try
//             {
//                 if (File.Exists(stateFile))
//                 {
//                     _logger.LogDebug("Loading state from file");
//                     using (var fs = File.OpenRead(stateFile))
//                     {
//                         _instaApi.LoadStateDataFromStream(fs);
//                     }
//                 }
//             }
//             catch (Exception e)
//             {
//                 _logger.LogCritical(e.Message);
//             }
//
//             if (!_instaApi.IsUserAuthenticated)
//             {
//                 var logInResult = _instaApi.LoginAsync().ConfigureAwait(false).GetAwaiter().GetResult();
//                 if (!logInResult.Succeeded)
//                 {
//                     _logger.LogDebug($"Unable to login: {logInResult.Info.Message}");
//                     return;
//                 }
//             }
//
//             var state = _instaApi.GetStateDataAsStream();
//             using (var fileStream = File.Create(stateFile))
//             {
//                 state.Seek(0, SeekOrigin.Begin);
//                 state.CopyTo(fileStream);
//                 _logger.LogDebug($"Instasharper state saved to: {stateFile}");
//             }
//
//             _logger.LogDebug(
//                 $"Instasharper library initialized. User '{_appConfig.Value.Username}' authenticated: {_instaApi.IsUserAuthenticated}");
//         }
//
//         public async Task<IEnumerable<InstagramPost>> GetUserPosts(string username)
//         {
//             _logger.LogDebug($"Loading posts for user '{username}'");
//             var getMediaResult =
//                 await _instaApi.GetUserMediaAsync(username, PaginationParameters.MaxPagesToLoad(3));
//             if (getMediaResult.Succeeded)
//             {
//                 var posts = new List<InstagramPost>();
//                 foreach (var media in getMediaResult.Value)
//                 {
//                     var post = _mapper.Map<InstagramPost>(media);
//                     post.Carousel = _mapper.Map<List<InstagramCarouselItem>>(media.Carousel as List<InstaCarouselItem>);
//                     if (post.Carousel != null)
//                     {
//                         foreach (var carouselItem in post.Carousel)
//                         {
//                             foreach (var carouselItemImage in carouselItem.Images)
//                             {
//                                 await ProcessImage(username, carouselItemImage, Path.Combine(post.Code, carouselItem.Pk));
//                             }
//                             foreach (var carouselItemVideo in carouselItem.Videos)
//                             {
//                                 await ProcessVideo(username, carouselItemVideo, Path.Combine(post.Code, carouselItem.Pk));
//                             }
//                         }
//                     }
//                     else
//                     {
//                         foreach (var instagramImage in post.Images)
//                         {
//                             await ProcessImage(username, instagramImage, post.Code);
//                         }
//
//                         foreach (var instagramVideo in post.Videos)
//                         {
//                             await ProcessVideo(username, instagramVideo, post.Code);
//                         }
//                     }
//                     posts.Add(post);
//                 }
//
//                 _logger.LogDebug($"Loaded {getMediaResult.Value.Count} posts for user '{username}'");
//                 return posts;
//             }
//
//             _logger.LogError($"Unable to load user '{username}' all posts: {getMediaResult.Info.Message}");
//             return new List<InstagramPost>();
//         }
//
//         private async Task ProcessVideo(string username, InstagramVideo instagramVideo, string postSubPath)
//         {
//             var videoStream =
//                 await _imageLoader.LoadVideoAsStream(new Uri(instagramVideo.Url)) as MemoryStream;
//
//             var filename = string.Concat(instagramVideo.Width.ToString(),
//                 "x",
//                 instagramVideo.Height.ToString(),
//                 ".mp4");
//             var relativePath = Path.Combine(username, postSubPath);
//             var relativeFileName = Path.Combine(relativePath, filename);
//             var localPath = Path.Combine(_appConfig.Value.FileStoragePath, relativePath);
//
//             if (!Directory.Exists(localPath))
//                 Directory.CreateDirectory(localPath);
//             var path = Path.Combine(localPath, filename);
//             if (!File.Exists(path))
//             {
//                 File.WriteAllBytes(path, videoStream.ToArray());
//             }
//
//             instagramVideo.Path = relativeFileName;
//         }
//
//         private async Task ProcessImage(string username, InstagramImage instagramImage, string postSubPath)
//         {
//             var image = await _imageLoader.LoadImage(new Uri(instagramImage.URI));
//             var filename = string.Concat(instagramImage.Width.ToString(),
//                 "x",
//                 instagramImage.Height.ToString(),
//                 ".jpg");
//             var relativePath = Path.Combine(username, postSubPath);
//             var relativeFileName = Path.Combine(relativePath, filename);
//             var localPath = Path.Combine(_appConfig.Value.FileStoragePath, relativePath);
//
//             if (!Directory.Exists(localPath))
//                 Directory.CreateDirectory(localPath);
//             var path = Path.Combine(localPath, filename);
//             if (!File.Exists(path))
//                 image.Save(path);
//             instagramImage.Path = relativeFileName;
//             instagramImage.ImageBytes = image.ToByteArray(ImageFormat.Jpeg);
//         }
//
//         public async Task<InstagramReelFeed> GetUserStory(string username)
//         {
//             _logger.LogDebug($"Loading story for user '{username}'");
//             var user = await _instaApi.GetUserAsync(username);
//
//             if (!user.Succeeded)
//             {
//                 _logger.LogDebug($"Loading to load user '{username}'");
//                 return new InstagramReelFeed();
//             }
//
//             var getStoryResult = await _instaApi.GetUserStoryFeedAsync(user.Value.Pk);
//             if (getStoryResult.Succeeded)
//             {
//                 _logger.LogDebug($"Loaded {getStoryResult.Value.Items.Count} stories for user '{username}'");
//                 return _mapper.Map<InstagramReelFeed>(getStoryResult.Value);
//             }
//
//             _logger.LogError($"Unable to load user '{username}' all posts: {getStoryResult.Info.Message}");
//             return new InstagramReelFeed();
//         }
//     }
// }