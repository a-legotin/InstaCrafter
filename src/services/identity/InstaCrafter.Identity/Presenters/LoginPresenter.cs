using System.Net;
using System.Text.Json;
using InstaCrafter.Core.UseCases;
using InstaCrafter.Identity.Core.Dto.UseCaseResponses;
using InstaCrafter.Identity.Core.Interfaces;

namespace InstaCrafter.Identity.Presenters
{
    public sealed class LoginPresenter : IOutputPort<LoginResponse>
    {
        public JsonContentResult ContentResult { get; }

        public LoginPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(LoginResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.Unauthorized);
            ContentResult.Content = response.Success ? JsonSerializer.Serialize(new Models.Response.LoginResponse(response.AccessToken, response.RefreshToken, response.User)) : JsonSerializer.Serialize(response.Errors);
        }
    }
}
