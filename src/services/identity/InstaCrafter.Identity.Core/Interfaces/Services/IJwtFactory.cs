using System.Threading.Tasks;
using InstaCrafter.Identity.Core.Dto;

namespace InstaCrafter.Identity.Core.Interfaces.Services
{
    public interface IJwtFactory
    {
        Task<AccessToken> GenerateEncodedToken(string id, string userName);
    }
}
