using System;
using InstaCrafter.Classes.Database;
using InstaCrafter.Classes.Wrapper;
using InstaCrafter.Core.Converters;
using InstaCrafter.Core.Interfaces;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    internal class ConvertersFabric
    {
        internal static IObjectConverter<InstaPostList, InstaResponse> GetPostsConverter(InstaResponse instaresponse)
        {
            return new InstaPostsConverter() { SourceObject = instaresponse };
        }

        internal static IObjectConverter<InstaUser, InstaResponseUser> GetUserConverter(InstaResponseUser instaresponse)
        {
            return new InstaUsersConverter() { SourceObject = instaresponse };
        }
    }
}