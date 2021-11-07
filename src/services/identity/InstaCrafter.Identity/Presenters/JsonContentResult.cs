using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Identity.Presenters
{
  public sealed class JsonContentResult : ContentResult
  {
    public JsonContentResult()
    {
      ContentType = "application/json";
    }
  }
}
