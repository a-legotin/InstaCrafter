namespace InstaCrafter.Infrastructure.Identity.Models.Authentication
{
    public class TokenResponse
    {
        public TokenResponse(ApplicationUser? user,
            string role,
            string token
            //string refreshToken
        )
        {
            Id = user?.Id ?? string.Empty;
            FullName = user?.FullName ?? string.Empty;
            EmailAddress = user?.Email ?? string.Empty;
            Token = token;
            Role = role;
        }

        public string? Id { get; set; }
        public string? FullName { get; set; }
        public string? EmailAddress { get; set; }
        public string? Token { get; set; }
        public string? Role { get; set; }
        public static TokenResponse Empty => new(null, string.Empty, string.Empty);
    }
}