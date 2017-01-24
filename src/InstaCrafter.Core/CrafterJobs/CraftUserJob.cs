namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftUserJob : ICraftJob
    {
        public int PagesCount { get; set; }
        public string UserName { get; set; }
    }
}