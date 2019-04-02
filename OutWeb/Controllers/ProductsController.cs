using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class ProductsController : WebUserController
    {
        public ActionResult Index()
        {
            return View("List");
        }
        public ActionResult List()
        {
            return View();
        }
        public ActionResult Content()
        {
            return View();
        }
    }
}