using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Classes.Wrapper;
using InstaCrafter.Core.Interfaces;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public class InstaPostsConverter : IObjectConverter<InstaPost, InstaResponseItem>
    {
        public InstaPost Convert(InstaResponseItem source)
        {
            return new InstaPost();
        }
    }
}
