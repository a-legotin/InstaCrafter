using System.Net;
using System.Text.Json;
using InstaCrafter.Core.UseCases;
using InstaCrafter.Tasks.Core.Dto.UseCaseResponses;

namespace InstaCrafter.Tasks.Presenters
{
    public sealed class AddIgAccountPresenter : IOutputPort<AddIgAccountResponse>
    { 
        public JsonContentResult ContentResult { get; }

        public AddIgAccountPresenter()
        {
            ContentResult = new JsonContentResult();
        }

        public void Handle(AddIgAccountResponse response)
        {
            ContentResult.StatusCode = (int)(response.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest);
            ContentResult.Content = JsonSerializer.Serialize(response);
        }
    }
}