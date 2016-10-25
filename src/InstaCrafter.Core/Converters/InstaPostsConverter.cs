using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;
using InstaCrafter.Classes.Wrapper;
using InstaCrafter.Core.Interfaces;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public class InstaPostsConverter : IObjectConverter<InstaPostList, InstaResponse>
    {
        public InstaResponse SourceObject { get; set; }

        public InstaPostList Convert()
        {
            if (SourceObject == null) throw new ArgumentNullException("Source object");
            var instaPosts = new InstaPostList();
            foreach (var post in SourceObject.Items)
            {
                instaPosts.Add(new InstaPost()
                {
                    Url = post.Link,
                    CanViewComment = post.CanViewComment,
                    Code = post.Code,
                    CreatedTime = post.CreatedTimeConverted
                });
            };
            return instaPosts;
        }
    }
}
