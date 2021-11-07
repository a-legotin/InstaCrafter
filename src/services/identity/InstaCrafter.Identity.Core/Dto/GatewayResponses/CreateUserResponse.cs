using System.Collections.Generic;
using InstaCrafter.Core.Dto;
using InstaCrafter.Core.GatewayResponses;

namespace InstaCrafter.Identity.Core.Dto.GatewayResponses
{
  public sealed class CreateUserResponse : BaseGatewayResponse
  {
    public string Id { get; }
    public CreateUserResponse(string id, bool success = false, IEnumerable<Error> errors = null) : base(success, errors)
    {
      Id = id;
    }
  }
}
