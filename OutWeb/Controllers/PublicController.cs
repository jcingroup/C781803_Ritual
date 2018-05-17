using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class PublicController : WebUserController
    {
        public ActionResult Index()
        {
            return View("Public");
        }
    }
}