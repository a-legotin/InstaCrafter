using InstaCrafter.API.CommandResponses;
using InstaCrafter.Infrastructure.Identity.Models.Authentication;
using MediatR;

namespace InstaCrafter.API.Commands
{
    public class AuthenticateCommand : TokenRequest, IRequest<CommandResponse>
    {
    }
}