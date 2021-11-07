using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using InstaCrafter.Identity.Core.Interfaces.Services;
using InstaCrafter.Identity.Infrastructure.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace InstaCrafter.Identity.Infrastructure.Auth
{
    internal sealed class JwtTokenHandler : IJwtTokenHandler
    {
        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler;
        private readonly Microsoft.Extensions.Logging.ILogger<JwtTokenHandler> _logger;

        public JwtTokenHandler(Microsoft.Extensions.Logging.ILogger<JwtTokenHandler> logger)
        {
            if (_jwtSecurityTokenHandler == null)
                _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();

            _logger = logger;
        }

        public string WriteToken(JwtSecurityToken jwt)
        {
            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }

        public ClaimsPrincipal ValidateToken(string token, TokenValidationParameters tokenValidationParameters)
        {
            try
            {
                var principal = _jwtSecurityTokenHandler.ValidateToken(token, tokenValidationParameters, out var securityToken);

                if (!(securityToken is JwtSecurityToken jwtSecurityToken) || !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
                    throw new SecurityTokenException("Invalid token");

                return principal;
            }
            catch (Exception e)
            {
                _logger.LogWarning($"Token validation failed: {e.Message}");
                return null;
            }
        }
    }
}
