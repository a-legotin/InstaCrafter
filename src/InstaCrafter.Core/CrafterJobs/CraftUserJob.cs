namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftUserJob : ICraftJob
    {
        public string Username { get; set; }
        public int PagesCount { get; set; }
    }
}