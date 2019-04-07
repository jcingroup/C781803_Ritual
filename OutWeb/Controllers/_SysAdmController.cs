﻿using Newtonsoft.Json;
using OutWeb.Authorize;
using OutWeb.Models.Manage.ProductModels;
using OutWeb.Models.Manage.TeamModels;
using OutWeb.Modules.Manage;
using OutWeb.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUser.Controller;

namespace OutWeb.Controllers
{
    [Auth]
    [ErrorHandler]
    public class _SysAdmController : WebUserController
    {
        public _SysAdmController()
        {
            ViewBag.IsFirstPage = false;
        }

        #region 全台服務團隊

        [HttpGet]
        public ActionResult TeamList(int? page, string qry, string sort, string disable, int? city, int? area)
        {
            TeamListViewModel model = new TeamListViewModel();
            model.Filter.CurrentPage = page ?? 1;
            model.Filter.QueryString = qry ?? string.Empty;
            model.Filter.SortColumn = sort ?? string.Empty;
            model.Filter.Disable = disable ?? string.Empty;
            model.Filter.CityID = city;
            model.Filter.AreaID = area;
            model.Filter.DoPagination = true;

            TeamModule mdu = new TeamModule();
            model.Result = mdu.DoGetList(model.Filter);

            TempData["CityData"] = mdu.GetCityData();
            return View(model);
        }

        [HttpGet]
        public ActionResult TeamDataAdd()
        {
            TeamDetailsDataModel defaultModel = new TeamDetailsDataModel();
            //defaultModel.Data.PUB_DT_STR = DateTime.UtcNow.AddHours(8).ToString("yyyy\\/MM\\/dd");
            //defaultModel.Data.DISABLE = false;
            defaultModel.Data.SQ = 1;
            //defaultModel.Data.HOME_PAGE_DISPLAY = true;
            TeamModule mdu = new TeamModule();
            var cities = mdu.GetCityData();
            TempData["cities"] = cities;
            return View(defaultModel);
        }

        public ActionResult TeamDataEdit(int? ID)
        {
            if (!ID.HasValue)
                return RedirectToAction("TeamList");

            TeamDetailsDataModel model = new TeamDetailsDataModel();
            TeamModule mdu = new TeamModule();

            model = mdu.DoGetDetailsByID((int)ID);
            var cities = mdu.GetCityData();
            TempData["cities"] = cities;

            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TeamDataSave(FormCollection form)
        {
            int? ID = Convert.ToInt32(form["pageId"]);
            int identityId = 0;
            TeamModule mdu = new TeamModule();

            identityId = mdu.DoSaveData(form, ID);

            return RedirectToAction("TeamDataEdit", new { ID = identityId });
        }

        [HttpPost]
        public JsonResult TeamDataDelete(int? ID)
        {
            bool success = true;
            string messages = string.Empty;
            try
            {
                TeamModule mdu = new TeamModule();
                mdu.DoDeleteByID((int)ID);

                messages = "刪除成功";
            }
            catch (Exception ex)
            {
                success = false;
                messages = ex.Message;
            }
            var resultJson = Json(new { success = success, messages = messages });
            return resultJson;
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

        #endregion 全台服務團隊

        #region 修改密碼

        //修改密碼
        [HttpGet]
        public ActionResult ChangePW()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ChangePW(FormCollection form)
        {
            SignInModule signInModule = new SignInModule();
            try
            {
                signInModule.ChangePassword(form);
                ViewBag.Message = "success";
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }
            finally
            {
                signInModule.Dispose();
            }
            return View();
        }

        #endregion 修改密碼

        #region 保健食品

        public ActionResult ProductsList()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ProductsAdd()
        {
            var today = DateTime.UtcNow.AddHours(8).ToString("yyyy\\-MM\\-dd");
            ProductDetailsDataModel defaultModel = new ProductDetailsDataModel();
            defaultModel.Data = new Entities.PRODUCT()
            {
                PUBLISH_DT = today,
                SORT = 1,
            };
            return View(defaultModel);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductsAdd(FormCollection form, List<HttpPostedFileBase> files)
        {
            int identityId = 0;
            using (var module = ListFactoryService.Create(Enums.ListMethodType.PRODUCT))
            {
                identityId = module.DoSaveData(form, null, files);
            }
            var redirectUrl = new UrlHelper(Request.RequestContext).Action("ProductsEdit", "_SysAdm", new { ID = identityId });
            return Json(new { Url = redirectUrl });
        }

        [HttpGet]
        public ActionResult ProductsEdit()
        {
            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ProductsEdit(FormCollection form, List<HttpPostedFileBase> files)
        {
            return View();
        }

        #endregion 保健食品
    }
}