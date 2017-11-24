using System.Threading.Tasks;

namespace InstaBackup.Scrapper
{
    public interface IScrapper<T>
    {
        Task<T> Scrap(string username);
    }
}
