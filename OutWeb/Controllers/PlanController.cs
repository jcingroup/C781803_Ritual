using OutWeb.Models.FrontEnd.NewsFrontEndModels;
using OutWeb.Modules.FontEnd;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class PlanController : Controller
    {
        public ActionResult Index()
        {
            return View("Plan");
        }
    }
}