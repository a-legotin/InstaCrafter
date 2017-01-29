using System.Web.Mvc;

namespace InstaCrafter.Core.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Jobs()
        {
            return View();
        }
    }
}