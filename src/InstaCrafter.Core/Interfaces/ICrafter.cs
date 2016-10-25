using System.Collections.Generic;
using System.Threading;
using InstaCrafter.Models;

namespace InstaCrafter.Core
{
    public interface ICrafter<T>
    {
        T CraftItem { get; set; }
        string DataStoreUrl { get; }
        CancellationToken CancelToken { get; }
        void Craft();
    }
}