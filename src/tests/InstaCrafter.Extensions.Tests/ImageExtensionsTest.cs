using System;
using System.Drawing;
using System.Drawing.Imaging;
using Xunit;

namespace InstaCrafter.Extensions.Tests
{
    public class ImageExtensionsTest
    {
        [Fact]
        public void Test_Image_To_Bytes_Should_Return_Not_Null()
        {
            int width = 100, height = 100;
            var bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
            var rand = new Random();

            for (var y = 0; y < height; y++)
            {
                for (var x = 0; x < width; x++)
                {
                    //generate random ARGB value
                    var a = rand.Next(256);
                    var r = rand.Next(256);
                    var g = rand.Next(256);
                    var b = rand.Next(256);

                    //set ARGB value
                    bmp.SetPixel(x, y, Color.FromArgb(a, r, g, b));
                }
            }

            var image = bmp.ToByteArray(ImageFormat.Bmp);
            Assert.True(image != null);
        }
    }
}