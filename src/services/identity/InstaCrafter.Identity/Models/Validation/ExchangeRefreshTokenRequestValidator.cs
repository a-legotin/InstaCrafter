using FluentValidation;
using InstaCrafter.Identity.Models.Request;

namespace InstaCrafter.Identity.Models.Validation
{
    public class ExchangeRefreshTokenRequestValidator : AbstractValidator<ExchangeRefreshTokenRequest>
    {
        public ExchangeRefreshTokenRequestValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty();
            RuleFor(x => x.RefreshToken).NotEmpty();
        }
    }
}
