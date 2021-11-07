using System.Collections.Generic;
using InstaCrafter.Core.Dto;

namespace InstaCrafter.Core.GatewayResponses
{
  public abstract class BaseGatewayResponse
  {
    public bool Success { get; }
    public IEnumerable<Error> Errors { get; }

    protected BaseGatewayResponse(bool success, IEnumerable<Error> errors)
    {
      Success = success;
      Errors = errors;
    }
  }
}

