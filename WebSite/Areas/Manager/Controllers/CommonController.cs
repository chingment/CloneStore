using Lumos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Lumos.Entity;
namespace WebSite.Areas.Manager.Controllers
{

    public class CommonController : ManagerBaseController
    {

        public ViewResult SelectClient()
        {
            return View();
        }


        public JsonResult GetClientList()
        {
            //string userName = Request.Form["txt_UserName"].ToString();
            //string realName = Request.Form["txt_RealName"].ToString();


            //var list = (from ur in CurrentDb.SysClientUser
            //            join u in CurrentDb.Users on ur.Id equals u.Id
            //            where
            //               ur.IsDelete == false && 
            //                (userName.Length == 0 || u.UserName.Contains(userName)) &&
            //                (realName.Length == 0 || ur.RealUserName.Contains(realName)) 
            //            select new { ur.Id, ur.RealUserName, u.UserName }).Distinct();

            //int total = list.Count();

            //int pageIndex = int.Parse(Request.Form["pageindex"].ToString());
            //int pageSize = 10;
            //list = list.OrderBy(r => r.UserName).Skip(pageSize * (pageIndex)).Take(pageSize);

            //PageEntity pageEntity = new PageEntity { PageSize = pageSize, TotalRecord = total, Rows = list };

            //return Json(ResultType.Success, pageEntity);

            return Json(ResultType.Success);
        }

      




        //[AllowAnonymous]
        //public JsonResult UploadImageToServer()
        //{
        //    CustomJsonResult r = new CustomJsonResult();
        //    try
        //    {
        //        string path = Request.QueryString["path"];
        //        if (path == null)
        //            path = "/temp";

        //        foreach (string f in Request.Files.AllKeys)
        //        {
        //            HttpPostedFileBase file = Request.Files[f];

        //            ImageUpload s = new ImageUpload();
        //            string domain = System.Configuration.ConfigurationManager.AppSettings["ImagesServerUrl"];
        //            string imagesServerUrl = CommonSetting.GetUploadPath(path);
        //            if (s.Save(file, domain, imagesServerUrl, ""))
        //            {
        //                r.Content = s;
        //            }

        //        }
        //        r.Result = ResultType.Success;
        //        r.Message = "上传成功";
        //    }
        //    catch (System.Exception ex)
        //    {
        //        r.Result = ResultType.Exception;
        //        r.Message = "上传失败";
        //        Log.Error("远程上传图片", ex);
        //    }

        //    return Json("text/html", r.Result, r.Content, r.Message);
        //}

        public JsonResult UploadImage(string fileInputName, string path, string oldFileName)
        {
            CustomJsonResult rm = new CustomJsonResult();
            rm.ContentType = "text/html";
            try
            {
                HttpPostedFileBase file_upload = Request.Files[fileInputName];

                if (file_upload == null)
                    return Json("text/html", ResultType.Failure, "上传失败");

                System.IO.FileInfo file = new System.IO.FileInfo(file_upload.FileName);
                if (file.Extension != ".jpg" && file.Extension != ".png" && file.Extension != ".gif" && file.Extension != ".bmp")
                {
                    return Json("text/html", ResultType.Failure, "上传的文件不是图片格式(jpg,png,gif,bmp)");
                }


                string strUrl = System.Configuration.ConfigurationManager.AppSettings["app_ImagesServerUploadUrl"] + "?date=" + DateTime.Now.ToString("yyyyMMddhhmmssfff");


                byte[] bytes = null;
                using (var binaryReader = new BinaryReader(file_upload.InputStream))
                {
                    bytes = binaryReader.ReadBytes(file_upload.ContentLength);
                }
                string fileExt = Path.GetExtension(file_upload.FileName).ToLower();
                UploadFileEntity entity = new UploadFileEntity();
                entity.FileName = DateTime.Now.ToString("yyyyMMddhhmmssfff") + fileExt;//自定义文件名称，这里以当前时间为例
                entity.FileData = bytes;
                entity.UploadFolder = path;
                rm = HttpClientOperate.Post<CustomJsonResult>(path, strUrl, entity);//封装的POST提交方
                rm.ContentType = "text/html";
                if (rm.Result == ResultType.Exception || rm.Result == ResultType.Unknown)
                {
                    rm.Message = "上传图片发生异常";
                }
            }
            catch (Exception ex)
            {
                rm.Result = ResultType.Exception;
                rm.Message = "上传图片发生异常";
                Log.Error(ex);
            }
            return rm;

        }

    }
}