using Microsoft.AspNetCore.Mvc;

namespace InstaCrafter.Tasks.Presenters
{
  public sealed class JsonContentResult : ContentResult
  {
    public JsonContentResult()
    {
      ContentType = "application/json";
    }
  }
}
