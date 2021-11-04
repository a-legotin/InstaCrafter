using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using InstaCrafter.Identity.Helpers;
using InstaCrafter.Identity.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace InstaCrafter.Identity.Auth
{
    public class JwtFactory : IJwtFactory
    {
        private readonly JwtIssuerOptions _jwtOptions;

        public JwtFactory(IOptions<JwtIssuerOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
            ThrowIfInvalidOptions(_jwtOptions);
        }

        public async Task<string> GenerateEncodedToken(string userName, ClaimsIdentity identity)
        {
            var now = DateTime.UtcNow;  
  
            var claims = new Claim[]  
            {  
                new Claim(JwtRegisteredClaimNames.Sub, userName),  
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),  
                new Claim(JwtRegisteredClaimNames.Iat, now.ToUniversalTime().ToString(), ClaimValueTypes.Integer64)  
            };  
  
            var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("dhgdfhdygh5346t3tfwfsdfsdsf"));  
            var tokenValidationParameters = new TokenValidationParameters  
            {  
                ValidateIssuerSigningKey = true,  
                IssuerSigningKey = signingKey,  
                ValidateIssuer = true,  
                ValidIssuer = _jwtOptions.Issuer,  
                ValidateAudience = true,  
                ValidAudience = _jwtOptions.Audience,  
                ValidateLifetime = true,  
                ClockSkew = TimeSpan.Zero,  
                RequireExpirationTime = true,  
  
            };  
  
            var jwt = new JwtSecurityToken(  
                issuer: _jwtOptions.Issuer,  
                audience: _jwtOptions.Audience,  
                claims: claims,  
                notBefore: now,  
                expires: now.Add(TimeSpan.FromHours(2)),  
                signingCredentials: new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256)  
            );  

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

        public ClaimsIdentity GenerateClaimsIdentity(string userName, string id)
        {
            return new ClaimsIdentity(new GenericIdentity(userName, "Token"), new[]
            {
                new Claim(Constants.Strings.JwtClaimIdentifiers.Id, id),
                new Claim(Constants.Strings.JwtClaimIdentifiers.Rol, Constants.Strings.JwtClaims.ApiAccess)
            });
        }

        private static long ToUnixEpochDate(DateTime date)
        {
            return (long) Math.Round((date.ToUniversalTime() -
                                      new DateTimeOffset(1970, 1, 1, 0, 0, 0, TimeSpan.Zero))
                .TotalSeconds);
        }

        private static void ThrowIfInvalidOptions(JwtIssuerOptions options)
        {
            if (options == null) throw new ArgumentNullException(nameof(options));

            if (options.ValidFor <= TimeSpan.Zero)
                throw new ArgumentException("Must be a non-zero TimeSpan.", nameof(JwtIssuerOptions.ValidFor));

            if (options.SigningCredentials == null)
                throw new ArgumentNullException(nameof(JwtIssuerOptions.SigningCredentials));

            if (options.JtiGenerator == null) throw new ArgumentNullException(nameof(JwtIssuerOptions.JtiGenerator));
        }
    }
}