using OutWeb.Enums;
using OutWeb.Models.Manage.AgentModels;
using OutWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class TeamController : Controller
    {
        public ActionResult Index()
        {
            return View("Team");
        }
    }
}