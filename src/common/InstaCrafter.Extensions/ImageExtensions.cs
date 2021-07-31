using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace InstaCrafter.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image imageIn, ImageFormat format)
        {
            using var ms = new MemoryStream();
            imageIn.Save(ms, format);
            return  ms.ToArray();
        }
    }
}