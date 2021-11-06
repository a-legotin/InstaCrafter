using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using InstaCrafter.Identity.Auth;
using InstaCrafter.Identity.Models;

namespace InstaCrafter.Identity.Helpers
{
    public static class Tokens
    {
        public static async Task<string> GenerateJwt(ClaimsIdentity identity, IJwtFactory jwtFactory, string userName,
            JwtIssuerOptions jwtOptions)
        {
            var response = new
            {
                id = identity.Claims.Single(c => c.Type == "id").Value,
                auth_token = await jwtFactory.GenerateEncodedToken(userName, identity),
                expires_in = (int)jwtOptions.ValidFor.TotalSeconds
            };

            return JsonSerializer.Serialize(response);
        }
    }
}