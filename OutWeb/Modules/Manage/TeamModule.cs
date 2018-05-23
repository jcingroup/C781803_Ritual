using OutWeb.Entities;
using OutWeb.Enums;
using OutWeb.Models;
using OutWeb.Models.Manage.TeamModels;
using OutWeb.Provider;
using OutWeb.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace OutWeb.Modules.Manage
{
    [Serializable]
    public class CityModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        private Dictionary<int, string> _Area = new Dictionary<int, string>();
        public Dictionary<int, string> Area { get { return _Area; } set { _Area = value; } }
    }

    /// <summary>
    /// 最新消息列表模組
    /// </summary>
    public class TeamModule
    {
        private List<CityModel> cities = new List<CityModel>();

        public List<CityModel> GetCityData()
        {
            List<CityModel> cities = new List<CityModel>();
            using (var db = new RITUAL())
            {
                var citiesSource = db.CITY.ToDictionary(d => d.ID, d => d.CITY_NM);
                foreach (var item in citiesSource)
                {
                    CityModel temp = new CityModel() { ID = item.Key, Name = item.Value };
                    var area = db.AREA.Where(s => s.MAP_CITY_ID == item.Key)
                        .ToDictionary(d => d.ID, d => d.AREA_NM);
                    temp.Area = area;
                    cities.Add(temp);
                }
            }
            return cities;
        }

        public void DoDeleteByID(int ID)
        {
            using (var db = new RITUAL())
            {
                var data = db.TEAM.Where(s => s.ID == ID).FirstOrDefault();
                if (data == null)
                    throw new Exception("[刪除護服務團隊] 查無此訊息，可能已被移除");
                try
                {
                    db.TEAM.Remove(data);
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception("[刪除護服務團隊]" + ex.Message);
                }
            }
        }

        public TeamDetailsDataModel DoGetDetailsByID(int ID)
        {
            TeamDetailsDataModel result = new TeamDetailsDataModel();
            using (var db = new RITUAL())
            {
                TEAM data = db.TEAM.Where(w => w.ID == ID).FirstOrDefault();
                PublicMethodRepository.HtmlDecode(data);
                result.Data = data;
            }

            return result;
        }

        public TeamListResultModel DoGetList(TeamListFilterModel filterModel)
        {
            PublicMethodRepository.FilterXss(filterModel);
            TeamListResultModel result = new TeamListResultModel();
            List<TEAM> data = new List<TEAM>();
            using (var db = new RITUAL())
            {
                try
                {
                    data = db.TEAM.ToList();

                    //關鍵字搜尋
                    if (!string.IsNullOrEmpty(filterModel.QueryString))
                    {
                        this.ListFilter(filterModel.QueryString, ref data);
                    }

                    if (filterModel.CityID.HasValue)
                    {
                        this.ListFilterCity(filterModel.CityID.Value, ref data);
                    }

                    if (filterModel.AreaID.HasValue)
                    {
                        this.ListFilterArea(filterModel.AreaID.Value, ref data);
                    }

                    //發佈日期搜尋
                    //if (!string.IsNullOrEmpty(filterModel.PublishDate))
                    //{
                    //    this.ListDateFilter(filterModel.PublishDate, ref data);
                    //}

                    //前台顯示
                    //if (!string.IsNullOrEmpty(filterModel.Disable))
                    //{
                    //    this.ListStatusFilter(filterModel.Disable, "Display", ref data);
                    //}

                    //上下架
                    //if (!string.IsNullOrEmpty(filterModel.Disable))
                    //{
                    //    this.ListStatusFilter(filterModel.Disable, "Disable", ref data);
                    //}

                    //排序
                    this.ListSort(filterModel.SortColumn, ref data);
                    PaginationResult pagination;
                    //分頁
                    this.ListPageList(filterModel.CurrentPage, ref data, out pagination);
                    result.Pagination = pagination;
                    foreach (var d in data)
                        PublicMethodRepository.HtmlDecode(d);

                    result.Data = data;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

            return result;
        }

        public int DoSaveData(FormCollection form, int? ID = null)
        {
            int identityId = 0;
            TEAM saveModel;
            using (var db = new RITUAL())
            {
                if (ID == 0)
                {
                    saveModel = new TEAM();
                    saveModel.BUD_ID = UserProvider.Instance.User.ID;
                    saveModel.BUD_DT = DateTime.UtcNow.AddHours(8);
                }
                else
                {
                    saveModel = db.TEAM.Where(s => s.ID == ID).FirstOrDefault();
                }
                bool tryStatus = false;
                bool status = true;
                if (bool.TryParse(form["fSt"], out tryStatus))
                {
                    status = Convert.ToBoolean(form["fSt"]);
                }

                saveModel.MAP_AREA_ID = Convert.ToInt32(form["area"]);
                saveModel.MAP_CITY_ID = Convert.ToInt32(form["city"]);
                saveModel.COMPANY_NM = form["companyName"];
                saveModel.ADDR = form["addr"];
                saveModel.CONTACT = form["contact"];
                saveModel.PHONE = form["phone"];
                saveModel.SQ = Convert.ToInt32(form["sq"]);
                saveModel.DISABLED = status;
                saveModel.UP_DT = DateTime.UtcNow.AddHours(8);
                saveModel.UP_ID = UserProvider.Instance.User.ID;
                PublicMethodRepository.FilterXss(saveModel);

                if (ID == 0)
                {
                    db.TEAM.Add(saveModel);
                }
                else
                {
                    db.Entry(saveModel).State = EntityState.Modified;
                }

                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                identityId = (int)saveModel.ID;
            }

            return identityId;
        }

        /// <summary>
        /// 列表關鍵字搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        private void ListFilter(string filterStr, ref List<TEAM> data)
        {
            var r = data.Where(s => s.COMPANY_NM.Contains(filterStr) || s.ADDR.Contains(filterStr)).ToList();
            data = r;
        }

        private void ListFilterCity(int cityID, ref List<TEAM> data)
        {
            var r = data.Where(s => s.MAP_CITY_ID == cityID).ToList();
            data = r;
        }

        private void ListFilterArea(int areaID, ref List<TEAM> data)
        {
            var r = data.Where(s => s.MAP_AREA_ID == areaID).ToList();
            data = r;
        }

        /// <summary>
        /// 日期條件搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        //private void ListDateFilter(string publishdate, ref List<TEAM> data)
        //{
        //    var r = data.Where(s => s.PUB_DT_STR == publishdate).ToList();
        //    data = r;
        //}

        /// <summary>
        /// 狀態搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        private void ListStatusFilter(string filter, string displayMode, ref List<TEAM> data)
        {
            List<TEAM> result = null;
            bool filterBooolean = Convert.ToBoolean(filter);
            if (displayMode == "DisplayHome")
            {
                result = data.Where(s => s.DISABLED == filterBooolean).ToList();
            }
            else if (displayMode == "Disable")
                result = data.Where(s => s.DISABLED == filterBooolean).ToList();
            data = result;
        }

        /// <summary>
        /// 取出分頁資料
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="data"></param>
        private void ListPageList(int currentPage, ref List<TEAM> data, out PaginationResult pagination)
        {
            int pageSize = (int)PageSizeConfig.SIZE10;
            int startRow = (currentPage - 1) * pageSize;
            PaginationResult paginationResult = new PaginationResult()
            {
                CurrentPage = currentPage,
                DataCount = data.Count,
                PageSize = pageSize,
                FirstPage = 1,
                LastPage = Convert.ToInt32(Math.Ceiling((decimal)data.Count / pageSize))
            };
            pagination = paginationResult;
            var query = data.Skip(startRow).Take(pageSize).ToList();
            data = query;
        }

        /// <summary>
        /// 列表排序功能
        /// </summary>
        /// <param name="sortCloumn"></param>
        /// <param name="data"></param>
        private void ListSort(string sortCloumn, ref List<TEAM> data)
        {
            switch (sortCloumn)
            {
                case "sortPublishCity/asc":
                    data = data.OrderBy(o => o.MAP_CITY_ID).ThenByDescending(o => o.SQ).ToList();
                    break;

                case "sortPublishCity/desc":
                    data = data.OrderByDescending(o => o.MAP_CITY_ID).ThenByDescending(o => o.SQ).ToList();
                    break;

                case "sortPublishArea/asc":
                    data = data.OrderBy(o => o.MAP_AREA_ID).ThenByDescending(o => o.SQ).ToList();
                    break;

                case "sortPublishArea/desc":
                    data = data.OrderByDescending(o => o.MAP_AREA_ID).ThenByDescending(o => o.SQ).ToList();
                    break;

                case "sortIndex/asc":
                    data = data.OrderByDescending(o => o.SQ).ThenByDescending(g => g.BUD_DT).ToList();
                    break;

                case "sortIndex/desc":
                    data = data.OrderBy(o => o.SQ).ThenByDescending(g => g.BUD_DT).ToList();
                    break;
                case "sortDisplayForFront/asc":
                    data = data.OrderBy(o => o.DISABLED).ThenByDescending(g => g.SQ).ToList();
                    break;

                case "sortDisplayForFront/desc":
                    data = data.OrderByDescending(o => o.DISABLED).ThenByDescending(g => g.SQ).ToList();
                    break;
                default:
                    data = data.OrderByDescending(o => o.SQ).ThenByDescending(g => g.BUD_DT).ToList();
                    break;
            }
        }
    }
}