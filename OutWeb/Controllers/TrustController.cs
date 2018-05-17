using OutWeb.Models.FrontEnd.ProductFrontEndModels;
using OutWeb.Modules.FontEnd;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class TrustController : Controller
    {
        public ActionResult Index()
        {
            return View("Trust");
        }
    }
}