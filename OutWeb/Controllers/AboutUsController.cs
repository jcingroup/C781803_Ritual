using OutWeb.Enums;
using OutWeb.Models.Manage.AgentModels;
using OutWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Controllers
{
    public class AboutUsController : Controller
    {
        public ActionResult Index()
        {
            return View("AboutUs");
        }
    }
}