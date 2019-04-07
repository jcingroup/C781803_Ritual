using OutWeb.Entities;
using System.Collections.Generic;

namespace OutWeb.Models.Manage.ProductModels
{
    public class ProductDetailsDataModel : IManage
    {
        public PRODUCT Data { get; set; } = new PRODUCT();

        public List<FileViewModel> FilesData { get; set; } = new List<FileViewModel>();
        public List<FileViewModel> ImagesData { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    }
}