using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class FlowController : WebUserController
    {
        public ActionResult Index()
        {
            return View("Flow");
        }
    }
}