using System.Threading.Tasks;
using CozyBus.Core.Bus;
using InstaCrafter.Core.UseCases;
using InstaCrafter.EventBus.Messages;
using InstaCrafter.Tasks.Core.Dto.UseCaseRequests;
using InstaCrafter.Tasks.Core.Dto.UseCaseResponses;
using InstaCrafter.Tasks.Core.Interfaces.UseCases;

namespace InstaCrafter.Tasks.Core.UseCases
{
    public class AddIgAccountUseCase : IAddIgAccountUseCase
    {
        private readonly IMessageBus _messageBus;

        public AddIgAccountUseCase(IMessageBus messageBus)
        {
            _messageBus = messageBus;
        }

        public async Task<bool> Handle(AddIgAccountRequest message, IOutputPort<AddIgAccountResponse> outputPort)
        {
            _messageBus.Publish<AuthenticateUserMessage>(new AuthenticateUserMessage(message.Username, message.Password));
            await Task.CompletedTask;
            outputPort.Handle(new AddIgAccountResponse(true));
            return true;
        }
    }
}