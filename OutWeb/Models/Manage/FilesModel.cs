using OutWeb.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models.Manage
{
    public class FilesModel
    {
        private List<int> m_oldFileIds = new List<int>();
        public List<int> OldFileIds { get { return this.m_oldFileIds; } set { this.m_oldFileIds = value; } }

        public int ID { get; set; }

        /// <summary>
        /// Action名稱
        /// </summary>
        public string ActionName { get; set; }
        public FileUploadType UploadIdentify { get; set; } = FileUploadType.NOTSET;

        /// <summary>
        /// 多筆
        /// </summary>

        private List<FileViewModel> m_memberDataMultiple = new List<FileViewModel>();
        public List<FileViewModel> MemberDataMultiple { get { return m_memberDataMultiple; } set { this.m_memberDataMultiple = value; } }
    }

}