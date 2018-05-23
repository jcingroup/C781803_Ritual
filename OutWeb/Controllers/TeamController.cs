using Newtonsoft.Json;
using OutWeb.Models.Manage.TeamModels;
using OutWeb.Modules.Manage;
using System;
using System.Linq;
using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    public class TeamController : WebUserController
    {
        [HttpGet]
        public ActionResult Index(int? page, string qry, string sort, string disable, int? city, int? area)
        {
            TeamListViewModel model = new TeamListViewModel();
            model.Filter.CurrentPage = page ?? 1;
            model.Filter.QueryString = qry ?? string.Empty;
            model.Filter.SortColumn = sort ?? string.Empty;
            model.Filter.Disable = disable ?? string.Empty;
            model.Filter.CityID = city;
            model.Filter.AreaID = area;

            TeamModule mdu = new TeamModule();
            model.Result = mdu.DoGetList(model.Filter);

            TempData["CityData"] = mdu.GetCityData();
            return View("Team", model);
        }

        [HttpPost]
        public JsonResult GetCityArea(int? ID)
        {
            CityModel result = new CityModel();
            string jsonDataStr = string.Empty;
            bool success = true;
            string messages = string.Empty;
            try
            {
                TeamModule mdu = new TeamModule();
                result = mdu.GetCityData().Where(s => s.ID == ID).First();
                jsonDataStr = JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                success = false;
                messages = ex.Message;
            }
            var resultJson = Json(new { success = success, data = jsonDataStr, messages = messages });
            return resultJson;
        }
    }
}