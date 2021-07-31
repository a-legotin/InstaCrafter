using System;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;

namespace InstaCrafter.Classes
{
    public interface IImageLoader
    {
        Task<Image> LoadImage(Uri uri);
        Task<Stream> LoadVideoAsStream(Uri uri);
    }
}