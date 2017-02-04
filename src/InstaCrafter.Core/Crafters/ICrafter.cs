using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Loggers;

namespace InstaCrafter.Core.Crafters
{
    public interface ICrafter
    {
        int Id { get; }
        ICraftLogger Logger { get; }
        ICraftJob Job { get; set; }
        string Name { get; }

        void Craft();
    }
}
