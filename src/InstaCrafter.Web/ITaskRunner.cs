using System.Threading.Tasks;

namespace InstaBackup
{
    public interface ITaskRunner
    {
        Task<bool> Run();
    }
}