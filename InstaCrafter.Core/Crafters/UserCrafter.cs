using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using InstaCrafter.Classes.Database;

namespace InstaCrafter.Core.Crafters
{
    public class UserCrafter : ICrafter<InstaUserList>
    {
        public InstaUserList CraftItem { get; set; }
        public string DataStoreUrl { get; }

        public CancellationToken CancelToken { get; }
        public UserCrafter(string dataStoreUrl, CancellationToken cancellationToken)
        {
            DataStoreUrl = dataStoreUrl;
            CancelToken = cancellationToken;
        }

        public void Craft()
        {
            throw new NotImplementedException();
        }
    }
}
