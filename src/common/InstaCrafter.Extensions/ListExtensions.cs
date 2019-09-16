using System;
using System.Collections.Generic;
using System.Linq;

namespace InstaCrafter.Extensions
{
    public static class ListExtensions
    {
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            var rnd = new Random(DateTime.Now.Millisecond);
            return source.OrderBy<T, int>((item) => rnd.Next());
        }
    }
}