namespace InstaCrafter.Core.CrafterJobs
{
    public interface ICraftJob
    {
        CraftJobKind Kind { get; }
        int Id { get; }
        CraftJobProgress Progress { get; set; }
    }
}