using System.Drawing;
using System.IO;

namespace InstaCrafter.Extensions
{
    public static class ImageExtensions
    {
        public static byte[] ToByteArray(this Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms,imageIn.RawFormat);
                return  ms.ToArray();
            }
        }
    }
}