using System.Collections.Generic;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public interface ICrafter<T>
    {
        List<T> Craft();
    }
}