using InstaCrafter.Infrastructure.Identity.Models.Authentication;

namespace InstaCrafter.API.CommandResponses
{
    public class CommandResponse
    {
        public CommandResponse() => Resource = TokenResponse.Empty;
        
        public TokenResponse Resource { get; set; }
    }
}