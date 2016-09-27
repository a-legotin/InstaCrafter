using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InstaCrafter.Classes.Wrapper
{
    public class InstaResponse
    {
        public bool IsFirstResponse { get; set; }
        public string Status { get; set; }

        public List<InstaResponseItem> Items { get; set; }
    }
}
