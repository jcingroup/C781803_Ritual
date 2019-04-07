using OutWeb.Models.Manage;
using OutWeb.Modules.Manage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Web;

namespace OutWeb.Repositories
{
    public class FileRepository
    {
        private string savePath { get; set; }
        private string rootPath { get { return HttpContext.Current.Server.MapPath("~/"); } }

        public FileRepository()
        {
        }

        public FileRepository(string savePath)
        {
            this.savePath = savePath;
        }

        #region 儲存處理函式

        /// <summary>
        ///
        /// </summary>
        /// <param name="vm"></param>
        public void SaveFileToDB(FilesModel vm)
        {
            FileModule fileModule = new FileModule();
            fileModule.SaveFiles(vm);
        }

        /// <summary>
        /// 上傳檔案
        /// </summary>
        /// <param name="vm"></param>
        /// <param name="myFile"></param>
        public void UploadFile(FilesModel vm, List<HttpPostedFileBase> files, int identityId, string siteName)
        {
            string serverMapPath = @"/Content/Upload/Manage/Files/" + siteName + "/" + identityId + @"/";

            if (files != null && files.Count > 0)
            {
                for (int i = 0; i < files.Count; i++)
                {
                    string strFileName = Guid.NewGuid().ToString() + Path.GetExtension(files[i].FileName);
                    string strFilePath = HttpContext.Current.Server.MapPath(serverMapPath + strFileName);
                    string realFileName = files[i].FileName;
                    string[] dirAry = new string[] { "Content", "Upload", "Manage", "Files", siteName, identityId.ToString() };
                    PublicMethodRepository.CreateDir(dirAry);
                    files[i].SaveAs(strFilePath);

                    #region data binding to model

                    vm.MemberDataMultiple.Add(new FileViewModel()
                    {
                        RealFileName = realFileName,
                        FilePath = strFilePath.Replace(rootPath, ""),
                        FileName = strFileName,
                        FileUrl = serverMapPath.Substring(1, serverMapPath.Length - 1) + strFileName,
                    });

                    #endregion data binding to model
                }
            }
        }

        #endregion 儲存處理函式

        #region Excel處理函式

        //public MemoryStream ObjectToExcel<TModel>(TModel obj) where TModel : ReplyBase
        //{
        //    Type type = obj.GetType();
        //    MemoryStream fs = new MemoryStream();
        //    switch (obj.GetExcelFormType)
        //    {
        //        case ExcelForm.EmptyForm1:
        //         var   model = obj.LayerData as List<律師名錄>;
        //            try
        //            {
        //                /* Excel Bind*/
        //                var wb = new XLWorkbook();
        //                var wsht = wb.Worksheets.Add("Sheet1");
        //                wsht.ColumnWidth = 16;
        //                wsht.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
        //                //Content
        //                for (int i = 0; i < model.Count; i++)
        //                {
        //                    wsht.Cell(i + 1, 1).Value = model[i].姓名;
        //                    wsht.Cell(i + 1, 2).Value = model[i].EMAIL;
        //                }

        //                wb.SaveAs(fs);
        //                fs.Position = 0;
        //                fs.Close();
        //                fs.Dispose();
        //                wsht.Dispose();
        //                wb.Dispose();
        //            }
        //            catch (Exception ex)
        //            {
        //                obj.IsSuccess = false;
        //                obj.Message = ex.Message;
        //            }
        //            obj.IsSuccess = true;
        //            break;
        //        case ExcelForm.EmptyForm2:
        //            break;
        //        case ExcelForm.EmptyForm3:
        //            break;
        //        case ExcelForm.EmptyForm4:
        //            break;
        //        case ExcelForm.EmptyForm5:
        //            break;
        //        default:
        //            break;
        //    }

        //    if (!obj.IsSuccess)
        //        throw new Exception(obj.Message);
        //    return fs;
        //}

        #endregion Excel處理函式
    }
}