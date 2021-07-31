using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace InstaCrafter.Classes
{
    public class ImageLoader : IImageLoader
    {
        private readonly ILogger<ImageLoader> _logger;

        public ImageLoader(ILogger<ImageLoader> logger)
        {
            _logger = logger;
        }

        public async Task<Image?> LoadImage(Uri uri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();
                        return new Bitmap(await response.Content.ReadAsStreamAsync());
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load the image");
            }

            return null;
        }
        
        public async Task<Stream?> LoadVideoAsStream(Uri uri)
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    using (var response = await client.GetAsync(uri))
                    {
                        response.EnsureSuccessStatusCode();
                        return await response.Content.ReadAsStreamAsync();
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to load the video");
            }

            return null;
        }
    }
}