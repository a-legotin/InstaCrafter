using System.Threading.Tasks;
using InstaCrafter.Core.Repositories;
using InstaCrafter.Identity.Core.Domain.Entities;
using InstaCrafter.Identity.Core.Dto.GatewayResponses;

namespace InstaCrafter.Identity.Core.Interfaces.Gateways.Repositories
{
    public interface IUserRepository  : IRepository<User>
    {
        Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName, string password);
        Task<User> FindByName(string userName);
        Task<bool> CheckPassword(User user, string password);
    }
}
