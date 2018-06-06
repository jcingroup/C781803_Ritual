namespace OutWeb.Models.Manage.TeamModels
{
    /// <summary>
    /// 最新消息列表資過濾條件模型
    /// </summary>
    public class TeamListFilterModel
    {
        public bool DoPagination { get; set; }
        public int? CityID { get; set; }
        public int? AreaID { get; set; }

        /// <summary>
        /// 選取頁面
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 查詢狀態
        /// </summary>
        public string Status { get; set; }


        /// <summary>
        /// 查詢關鍵字
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 排序條件
        /// </summary>
        public string SortColumn { get; set; }



        /// <summary>
        /// 上下架
        /// </summary>
        public string Disable { get; set; }

    }
}