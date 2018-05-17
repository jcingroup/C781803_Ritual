using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class PlanController : WebUserController
    {
        public ActionResult Index()
        {
            return View("Plan");
        }
    }
}