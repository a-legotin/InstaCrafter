using InstaCrafter.Core.UseCases;
using InstaCrafter.Tasks.Core.Dto.UseCaseResponses;

namespace InstaCrafter.Tasks.Core.Dto.UseCaseRequests
{
    public class AddIgAccountRequest : IUseCaseRequest<AddIgAccountResponse>
    {
        public AddIgAccountRequest(string username, string password)
        {
            Username = username;
            Password = password;
        }

        public string Username { get; }
        
        public string Password { get; }
    }
}