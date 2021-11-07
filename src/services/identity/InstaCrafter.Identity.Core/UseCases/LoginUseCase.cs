using System.Threading.Tasks;
using InstaCrafter.Core.Dto;
using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Dto;
using InstaCrafter.Identity.Core.Dto.UseCaseRequests;
using InstaCrafter.Identity.Core.Dto.UseCaseResponses;
using InstaCrafter.Identity.Core.Interfaces;
using InstaCrafter.Identity.Core.Interfaces.Gateways.Repositories;
using InstaCrafter.Identity.Core.Interfaces.Services;
using InstaCrafter.Identity.Core.Interfaces.UseCases;

namespace InstaCrafter.Identity.Core.UseCases
{
    public sealed class LoginUseCase : ILoginUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtFactory _jwtFactory;
        private readonly ITokenFactory _tokenFactory;

        public LoginUseCase(IUserRepository userRepository, IJwtFactory jwtFactory, ITokenFactory tokenFactory)
        {
            _userRepository = userRepository;
            _jwtFactory = jwtFactory;
            _tokenFactory = tokenFactory;
        }

        public async Task<bool> Handle(LoginRequest message, IOutputPort<LoginResponse> outputPort)
        {
            if (!string.IsNullOrEmpty(message.UserName) && !string.IsNullOrEmpty(message.Password))
            {
                var user = await _userRepository.FindByName(message.UserName);
                if (user != null)
                {
                    if (await _userRepository.CheckPassword(user, message.Password))
                    {
                        var refreshToken = _tokenFactory.GenerateToken();
                        user.AddRefreshToken(refreshToken, user.Id, message.RemoteIpAddress);
                        await _userRepository.Update(user);

                        outputPort.Handle(new LoginResponse(await _jwtFactory.GenerateEncodedToken(user.IdentityId, user.UserName), refreshToken, user, true));
                        return true;
                    }
                }
            }
            outputPort.Handle(new LoginResponse(new[] { new Error("login_failure", "Invalid username or password.") }));
            return false;
        }
    }
}
