using InstaCrafter.Core.CrafterJobs;
using InstaCrafter.Core.Loggers;
using Microsoft.AspNet.SignalR;

namespace InstaCrafter.Core.Crafters
{
    public interface ICrafter
    {
        int Id { get; }
        ICraftLogger Logger { get; }
        ICraftJob Job { get; set; }
        IHubContext ProgressReporter { get; }

        string Name { get; }

        void Craft();
    }
}