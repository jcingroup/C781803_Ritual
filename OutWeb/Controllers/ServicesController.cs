using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class ServicesController : Controller
    {
        public ServicesController()
        {
            ViewBag.IsFirstPage = false;
        }
        public ActionResult Index()
        {
            return View("Services");
        }
    }
}