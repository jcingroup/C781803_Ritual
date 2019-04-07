using OutWeb.Entities;
using OutWeb.Enums;
using OutWeb.Models.Manage;
using OutWeb.Provider;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace OutWeb.Modules.Manage
{
    public class FileModule : IDisposable
    {
        private string rootPath { get { return HttpContext.Current.Server.MapPath("~/"); } }

        private RITUAL DB { get; set; } = new RITUAL();

        /// <summary>
        /// 儲存圖片
        /// </summary>
        /// <param name="model"></param>
        public void SaveFiles(FilesModel model)
        {
            if (model.ID > 0)
            {
                List<FILEBASE> filterRemove = new List<FILEBASE>();
                if (model.UploadIdentify == FileUploadType.NOTSET)
                {
                    filterRemove = this.DB.FILEBASE
                                        .Where(o => !model.OldFileIds.Contains(o.ID) &&
                                        o.MAP_ID == model.ID &&
                                        o.MAP_SITE == model.ActionName &&
                                        o.FILE_TP == "F"
                                        ).ToList();
                }
                else
                {
                    filterRemove = this.DB.FILEBASE
                   .Where(o => !model.OldFileIds.Contains(o.ID) &&
                   o.MAP_ID == model.ID &&
                   o.MAP_SITE == model.ActionName &&
                   o.FILE_TP == "F" &&
                   o.IDENTIFY_KEY == (int)model.UploadIdentify
                   ).ToList();
                }

                if (filterRemove.Count > 0)
                {
                    foreach (var f in filterRemove)
                        File.Delete(string.Concat(rootPath, f.FILE_PATH));
                    //刪除舊檔
                    this.DB.FILEBASE.RemoveRange(filterRemove);
                    this.DB.SaveChanges();
                }
            }

            //存檔多筆
            foreach (var f in model.MemberDataMultiple)
            {
                int sq = model.MemberDataMultiple.IndexOf(f) + 1;
                FILEBASE file = new FILEBASE()
                {
                    FILE_RANDOM_NM = f.FileName,
                    MAP_SITE = model.ActionName,
                    SQ = sq,
                    BUD_DT = DateTime.UtcNow.AddHours(8),
                    BUD_ID = UserProvider.Instance.User.ID,
                    MAP_ID = model.ID,
                    FILE_REL_NM = f.RealFileName,
                    FILE_TP = "F",
                    FILE_PATH = f.FilePath,
                    URL_PATH = f.FileUrl,
                    IDENTIFY_KEY = model.UploadIdentify == FileUploadType.NOTSET ? default(int?) : (int)model.UploadIdentify,
                };
                this.DB.FILEBASE.Add(file);
                this.DB.SaveChanges();
                f.ID = file.ID;
            }
        }

        /// <summary>
        /// 取得FILEBASE
        /// </summary>
        /// <param name="ID"></param>
        /// <param name="actionName"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public List<FileViewModel> GetFiles(int ID, string actionName, FileUploadType fileType = FileUploadType.NOTSET)
        {

            List<FileViewModel> fileList = new List<FileViewModel>();
            fileList = this.DB.FILEBASE
                    .Where(o => o.MAP_ID == ID
                    && o.MAP_SITE.StartsWith(actionName)
                    && o.FILE_TP == "F"
                    && (fileType == FileUploadType.NOTSET ? true : o.IDENTIFY_KEY == (int)fileType)
                    )
                    .Select(s => new FileViewModel()
                    {
                        ID = s.ID,
                        FileName = s.FILE_RANDOM_NM,
                        RealFileName = s.FILE_REL_NM,
                        FilePath = s.FILE_PATH,
                        FileUrl = s.URL_PATH,
                    })
                    .ToList();

            return fileList;
        }

        public void Dispose()
        {
            if (this.DB.Database.Connection.State == System.Data.ConnectionState.Open)
            {
                this.DB.Database.Connection.Close();
            }
            this.DB.Dispose();
            this.DB = null;
        }
    }

}