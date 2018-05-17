using BotDetect.Web.Mvc;
using OutWeb.Models.UserInfo;
using OutWeb.Provider;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OutWeb.Controllers
{
    public class ContactUsController : Controller
    {
        public ActionResult Index()
        {
            return View("ContactUs");
        }
    }
}