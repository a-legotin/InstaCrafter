namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftUserJob : ICraftJob
    {
        public CraftUserJob(int id)
        {
            Id = id;
            Kind = CraftJobKind.Users;
        }

        public int PagesCount { get; set; }
        public string UserName { get; set; }
        public CraftJobKind Kind { get; }
        public int Id { get; }
        public CraftJobProgress Progress { get; set; } = new CraftJobProgress();
    }
}