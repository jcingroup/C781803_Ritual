using OutWeb.Enums;
using OutWeb.Models.FrontEnd.NewsFrontEndModels;
using OutWeb.Models.Manage.ManageNewsModels;
using OutWeb.Modules.FontEnd;
using OutWeb.Modules.Manage;
using OutWeb.Service;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Home");
        }
    }
}