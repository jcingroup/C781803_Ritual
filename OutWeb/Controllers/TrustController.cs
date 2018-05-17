using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class TrustController : WebUserController
    {
        public ActionResult Index()
        {
            return View("Trust");
        }
    }
}