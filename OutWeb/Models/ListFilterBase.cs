using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models
{
    public class ListFilterBase
    {
        /// <summary>
        /// 選取頁面
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// 查詢狀態
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 分類ID
        /// </summary>
        public int? CategoryID { get; set; }

        /// <summary>
        /// 查詢關鍵字
        /// </summary>
        public string QueryString { get; set; }

        /// <summary>
        /// 排序條件
        /// </summary>
        public string SortColumn { get; set; }

        /// <summary>
        /// 發稿日期(起)
        /// </summary>
        public string PublishStartDate { get; set; }

        /// <summary>
        /// 發稿日期(迄)
        /// </summary>
        public string PublishEndate { get; set; }

        /// <summary>
        /// 前台顯示
        /// </summary>
        public string DisplayForFrontEnd { get; set; }
        /// <summary>
        /// 上下架(是否停用)
        /// </summary>
        public string Disable { get; set; }

    }

}