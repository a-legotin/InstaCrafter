using System;
using System.IO;
using System.Threading.Tasks;
using SixLabors.ImageSharp;

namespace InstaCrafter.Classes
{
    public interface IImageLoader
    {
        Task<Image?> LoadImage(Uri uri);
        Task<Stream?> LoadVideoAsStream(Uri uri);
    }
}