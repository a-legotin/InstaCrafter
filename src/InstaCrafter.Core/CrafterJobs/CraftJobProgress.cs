namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftJobProgress
    {
        public int Maximum { get; set; }
        public int Current { get; set; }
        public CraftJobStatus Status { get; set; }
    }
}