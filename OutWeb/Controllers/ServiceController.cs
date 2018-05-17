using OutWeb.Models.FrontEnd.WorksFrontEndModels;
using OutWeb.Modules.FontEnd;
using System.Collections.Generic;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class ServiceController : Controller
    {
        public ActionResult Index()
        {
            return View("Service");
        }
    }
}