using System.Linq;
using System.Threading.Tasks;
using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Dto.UseCaseRequests;
using InstaCrafter.Identity.Core.Dto.UseCaseResponses;
using InstaCrafter.Identity.Core.Interfaces;
using InstaCrafter.Identity.Core.Interfaces.Gateways.Repositories;
using InstaCrafter.Identity.Core.Interfaces.UseCases;

namespace InstaCrafter.Identity.Core.UseCases
{
    public sealed class RegisterUserUseCase : IRegisterUserUseCase
    {
        private readonly IUserRepository _userRepository;

        public RegisterUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> Handle(RegisterUserRequest message, IOutputPort<RegisterUserResponse> outputPort)
        {
            var response = await _userRepository.Create(message.FirstName, message.LastName,message.Email, message.UserName, message.Password);
            outputPort.Handle(response.Success ? new RegisterUserResponse(response.Id, true) : new RegisterUserResponse(response.Errors.Select(e => e.Description)));
            return response.Success;
        }
    }
}
