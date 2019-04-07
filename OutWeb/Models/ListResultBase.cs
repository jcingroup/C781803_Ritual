using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models
{
    public class ListResultBase : IPaginationModel
    {

        public object Data { get; set; }

        /// <summary>
        /// 分頁模型
        /// </summary>
        public PaginationResult m_pagination = new PaginationResult();
        public PaginationResult Pagination { get { return this.m_pagination; } set { this.m_pagination = value; } }
    }
}