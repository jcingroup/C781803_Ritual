using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models
{
    public interface IListFilter<TData>
    {
        /// <summary>
        /// 列表關鍵字搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        List<TData> DoListFilterStringQuery(string filterStr, List<TData> data);

        /// <summary>
        /// 日期條件搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        List<TData> DoListDateFilter(DateTime publishSdate, DateTime publishEdate, List<TData> data);

        /// <summary>
        /// 分類篩選
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        List<TData> DoListCategoryFilter(int cateID, List<TData> data);

        /// <summary>
        /// 前台顯示搜尋
        /// </summary>
        /// <param name="filterStr"></param>
        /// <param name="data"></param>
        List<TData> DoListStatusFilter(string status, string displayMode, List<TData> data);

        /// <summary>
        /// 取出分頁資料
        /// </summary>
        /// <param name="currentPage"></param>
        /// <param name="data"></param>
        List<TData> DoListPageList(int currentPage, List<TData> data, out PaginationResult pagination, bool doPagination = true);

        /// <summary>
        /// 列表排序功能
        /// </summary>
        /// <param name="sortCloumn"></param>
        /// <param name="data"></param>
        List<TData> DoListSort(string sortCloumn, string status, List<TData> data);
    }

}