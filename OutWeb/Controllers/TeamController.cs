using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class TeamController : WebUserController
    {
        public ActionResult Index()
        {
            return View("Team");
        }
    }
}