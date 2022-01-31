using InstaCrafter.Core.UseCases;

namespace InstaCrafter.Tasks.Core.Dto.UseCaseResponses
{
    public class AddIgAccountResponse : UseCaseResponseMessage 
    {
        public AddIgAccountResponse(bool success) : base(success)
        {
        }
    }
}