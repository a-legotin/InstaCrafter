using System.Web.Mvc;

namespace InstaCrafter.Core.Controllers
{
    public class PostsController : Controller
    {
        // GET: Posts
        public ActionResult Posts()
        {
            return View();
        }
    }
}