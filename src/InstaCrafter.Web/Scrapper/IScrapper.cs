using System.Threading.Tasks;

namespace InstaCrafter.Web.Scrapper
{
    public interface IScrapper<T>
    {
        Task<T> Scrap(string username);
    }
}
