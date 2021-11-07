using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Interfaces;

namespace InstaCrafter.Identity.Core.Dto.UseCaseResponses
{
    public class ExchangeRefreshTokenResponse : UseCaseResponseMessage
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public ExchangeRefreshTokenResponse(bool success = false, string message = null) : base(success, message)
        {
        }

        public ExchangeRefreshTokenResponse(AccessToken accessToken, string refreshToken, bool success = false, string message = null) : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
