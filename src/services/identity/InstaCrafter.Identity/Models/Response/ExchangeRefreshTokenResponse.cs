using InstaCrafter.Identity.Core.Dto;

namespace InstaCrafter.Identity.Models.Response
{
    public class ExchangeRefreshTokenResponse
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public ExchangeRefreshTokenResponse(AccessToken accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
        }
    }
}
