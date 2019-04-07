using OutWeb.Models.Manage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OutWeb.Models
{
    public interface IManage
    {
        List<FileViewModel> FilesData { get; set; }

        List<FileViewModel> ImagesData { get; set; }
    }
}