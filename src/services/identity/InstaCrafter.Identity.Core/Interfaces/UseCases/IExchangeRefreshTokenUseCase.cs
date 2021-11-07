using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Dto.UseCaseRequests;
using InstaCrafter.Identity.Core.Dto.UseCaseResponses;

namespace InstaCrafter.Identity.Core.Interfaces.UseCases
{
    public interface IExchangeRefreshTokenUseCase : IUseCaseRequestHandler<ExchangeRefreshTokenRequest, ExchangeRefreshTokenResponse>
    {
    }
}
