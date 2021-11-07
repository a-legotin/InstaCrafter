 

using InstaCrafter.Identity.Core.Domain.Entities;
using InstaCrafter.Identity.Core.Dto;

namespace InstaCrafter.Identity.Models.Response
{
    public class LoginResponse
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }

        public User User { get; }
        public LoginResponse(AccessToken accessToken, string refreshToken, User user)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
        }
    }
}
