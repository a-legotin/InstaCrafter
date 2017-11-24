using System.Threading.Tasks;

namespace InstaCrafter.Web
{
    public interface ITaskRunner
    {
        Task<bool> Run();
    }
}