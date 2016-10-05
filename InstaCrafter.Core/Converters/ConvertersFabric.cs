using System;
using InstaCrafter.Classes.Wrapper;
using InstaCrafter.Core.Interfaces;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaPost, InstaResponseItem> GetPostsConverter(InstaResponse instaresponse)
        {
            return new InstaPostsConverter();
        }
    }
}