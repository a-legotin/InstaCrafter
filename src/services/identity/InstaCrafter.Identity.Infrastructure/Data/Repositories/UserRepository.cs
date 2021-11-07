using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using InstaCrafter.Core.Dto;
using InstaCrafter.Identity.Core.Domain.Entities;
using InstaCrafter.Identity.Core.Dto.GatewayResponses;
using InstaCrafter.Identity.Core.Interfaces.Gateways.Repositories;
using InstaCrafter.Identity.Core.Specifications;
using InstaCrafter.Identity.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace InstaCrafter.Identity.Infrastructure.Data.Repositories
{
    internal sealed class UserRepository : EfRepository<User>, IUserRepository
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;
        
        private AppDbContext AppDbContext => DbContext as AppDbContext;

        public UserRepository(UserManager<AppUser> userManager, IMapper mapper, AppDbContext dbContext): base(dbContext)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        public async Task<CreateUserResponse> Create(string firstName, string lastName, string email, string userName, string password)
        {
            var appUser = new AppUser {Email = email, UserName = userName};
            var identityResult = await _userManager.CreateAsync(appUser, password);

            if (!identityResult.Succeeded) return new CreateUserResponse(appUser.Id, false,identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
          
            var user = new User(firstName, lastName, appUser.Id, appUser.UserName);
            AppDbContext.Users.Add(user);
            await AppDbContext.SaveChangesAsync();

            return new CreateUserResponse(appUser.Id, identityResult.Succeeded, identityResult.Succeeded ? null : identityResult.Errors.Select(e => new Error(e.Code, e.Description)));
        }

        public async Task<User> FindByName(string userName)
        {
            var appUser = await _userManager.FindByNameAsync(userName);
            return appUser == null ? null : _mapper.Map(appUser, await GetSingleBySpec(new UserSpecification(appUser.Id)));
        }

        public async Task<bool> CheckPassword(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(_mapper.Map<AppUser>(user), password);
        }
    }
}
