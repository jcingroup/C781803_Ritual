using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class ContactUsController : WebUserController
    {
        public ActionResult Index()
        {
            return View("ContactUs");
        }
    }
}