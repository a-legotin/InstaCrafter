namespace InstaCrafter.Core.CrafterJobs
{
    public class CraftMediaJob : ICraftJob
    {
        public CraftMediaJob(string username, int id)
        {
            Id = id;
            UserName = username;
            Kind = CraftJobKind.Media;
            Progress = new CraftJobProgress();
        }

        public int PagesCount { get; set; }

        public string UserName { get; set; }
        public CraftJobKind Kind { get; }
        public int Id { get; }
        public CraftJobProgress Progress { get; set; }
    }
}