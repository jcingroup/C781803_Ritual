using OutWeb.Entities;
using OutWeb.Enums;
using OutWeb.Models;
using OutWeb.Modules.Manage;

namespace OutWeb.Service
{
    public class ListFactoryService
    {
        public static ListBaseService Create(ListMethodType methodType)
        {
            ListBaseService listManageModule = null;
            switch (methodType)
            {
                case ListMethodType.PRODUCT:
                    listManageModule = new ProductsModule<ListFilterBase, PRODUCT>();
                    break;

                default:
                    listManageModule = null;
                    break;
            }
            return listManageModule;
        }
    }
}