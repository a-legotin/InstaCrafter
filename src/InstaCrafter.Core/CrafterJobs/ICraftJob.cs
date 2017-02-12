namespace InstaCrafter.Core.CrafterJobs
{
    public interface ICraftJob
    {
        string UserName { get; set; }

        CraftJobKind Kind { get; }

        int Id { get; }
        CraftJobProgress Progress { get; set; }
    }
}