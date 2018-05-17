using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class AboutUsController : WebUserController
    {
        public ActionResult Index()
        {
            return View("AboutUs");
        }
    }
}