using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class HomeController : WebUserController
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}