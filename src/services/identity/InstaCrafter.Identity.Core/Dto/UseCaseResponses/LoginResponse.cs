using System.Collections.Generic;
using InstaCrafter.Core.Dto;
using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Domain.Entities;
using InstaCrafter.Identity.Core.Interfaces;

namespace InstaCrafter.Identity.Core.Dto.UseCaseResponses
{
    public class LoginResponse : UseCaseResponseMessage
    {
        public AccessToken AccessToken { get; }
        public string RefreshToken { get; }
        public User User { get; }
        public new IEnumerable<Error> Errors { get; }

        public LoginResponse(IEnumerable<Error> errors, bool success = false, string message = null) : base(success, message)
        {
            Errors = errors;
        }

        public LoginResponse(AccessToken accessToken, string refreshToken, User user, bool success = false, string message = null) : base(success, message)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            User = user;
        }
    }
}
