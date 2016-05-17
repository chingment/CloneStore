using Lumos.Common;
using Lumos.DAL;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Principal;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using System.IO;
using Newtonsoft.Json.Converters;
using Lumos.Mvc;

namespace WebSite.Areas.Manager
{

    /// <summary>
    /// BaseController用来扩展Controller,凡是在都该继承BaseController
    /// </summary>
    [ManagerException]
    [ManagerAuthorize]
    [ValidateInput(false)]
    public class ManagerController : BaseController
    {
        #region 公共的方法
        public string ConvertToZTreeJson(object obj, string idField, string pIdField, string nameField, string IconSkinField)
        {
            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            Type t = obj.GetType();
            foreach (var model in (object[])obj)
            {
                Type t1 = model.GetType();
                Json.Append("{");
                foreach (PropertyInfo p in t1.GetProperties())
                {
                    string name = p.Name.Trim().ToLower();
                    object value = p.GetValue(model, null);
                    if (name == idField.ToLower())
                    {
                        Json.Append("\"id\":" + JsonConvert.SerializeObject(value) + ",\"open\":true,");
                    }
                    else if (name == pIdField.Trim().ToLower())
                    {
                        Json.Append("\"pId\":" + JsonConvert.SerializeObject(value) + ",");

                        if (value == null || value.ToString() == "")
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "\" ");
                            Json.Append(",");
                        }
                        else
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "s\" ");
                            Json.Append(",");
                        }

                    }
                    else if (name == nameField.Trim().ToLower())
                    {
                        Json.Append("\"name\":" + JsonConvert.SerializeObject(value) + ",");

                    }
                    else
                    {
                        Json.Append("\"" + p.Name + "\":" + JsonConvert.SerializeObject(value) + ",");
                    }
                }
                if (Json.Length > 2)
                {
                    Json.Remove(Json.Length - 1, 1);
                }
                Json.Append("},");
            }
            if (Json.Length > 2)
            {
                Json.Remove(Json.Length - 1, 1);
            }
            Json.Append("]");
            return Json.ToString();
        }

        public string ConvertToZTreeJson(object obj, int[] isCheckedIds, string idField, string pIdField, string nameField, string IconSkinField)
        {

            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            Type t = obj.GetType();
            foreach (var model in (object[])obj)
            {
                Type t1 = model.GetType();
                Json.Append("{");
                foreach (PropertyInfo p in t1.GetProperties())
                {
                    string name = p.Name.Trim().ToLower();
                    object value = p.GetValue(model, null);
                    if (name == idField.ToLower())
                    {
                        Json.Append("\"id\":" + JsonConvert.SerializeObject(value) + ",\"open\":true,");
                        int v = int.Parse(value.ToString());
                        if (isCheckedIds.Contains(v))
                        {
                            Json.Append("\"checked\":true,");
                        }
                    }
                    else if (name == pIdField.Trim().ToLower())
                    {
                        Json.Append("\"pId\":" + JsonConvert.SerializeObject(value) + ",");
                        if (value == null || value.ToString() == "")
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "\" ");
                            Json.Append(",");
                        }
                        else
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "s\" ");
                            Json.Append(",");
                        }
                    }
                    else if (name == nameField.Trim().ToLower())
                    {
                        Json.Append("\"name\":" + JsonConvert.SerializeObject(value) + ",");
                    }
                    else
                    {
                        Json.Append("\"" + name + "\":" + JsonConvert.SerializeObject(value) + ",");
                    }
                }
                if (Json.Length > 2)
                {
                    Json.Remove(Json.Length - 1, 1);
                }
                Json.Append("},");
            }
            if (Json.Length > 2)
            {
                Json.Remove(Json.Length - 1, 1);
            }
            Json.Append("]");
            return Json.ToString();
        }

        public string ConvertToZTreeJson(object obj, string[] isCheckedIds, string idField, string pIdField, string nameField, string IconSkinField)
        {

            StringBuilder Json = new StringBuilder();
            Json.Append("[");
            Type t = obj.GetType();
            foreach (var model in (object[])obj)
            {
                Type t1 = model.GetType();
                Json.Append("{");
                foreach (PropertyInfo p in t1.GetProperties())
                {
                    string name = p.Name.Trim().ToLower();
                    object value = p.GetValue(model, null);
                    if (name == idField.ToLower())
                    {
                        Json.Append("\"id\":" + JsonConvert.SerializeObject(value) + ",\"open\":true,");
                        if (isCheckedIds.Contains(value.ToString()))
                        {
                            Json.Append("\"checked\":true,");
                        }
                    }
                    else if (name == pIdField.Trim().ToLower())
                    {
                        Json.Append("\"pId\":" + JsonConvert.SerializeObject(value) + ",");
                        if (value == null || value.ToString() == "")
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "\" ");
                            Json.Append(",");
                        }
                        else
                        {
                            Json.Append("\"iconSkin\":\"" + IconSkinField + "s\" ");
                            Json.Append(",");
                        }
                    }
                    else if (name == nameField.Trim().ToLower())
                    {
                        Json.Append("\"name\":" + JsonConvert.SerializeObject(value) + ",");
                    }
                    else
                    {
                        Json.Append("\"" + name + "\":" + JsonConvert.SerializeObject(value) + ",");
                    }
                }
                if (Json.Length > 2)
                {
                    Json.Remove(Json.Length - 1, 1);
                }
                Json.Append("},");
            }
            if (Json.Length > 2)
            {
                Json.Remove(Json.Length - 1, 1);
            }
            Json.Append("]");
            return Json.ToString();
        }
        #endregion 公共的方法

        private LumosDbContext _currentDb;
        public LumosDbContext CurrentDb
        {
            get
            {
                return _currentDb;
            }
        }

        protected ILog Log
        {
            get
            {
                return LogManager.GetLogger(this.GetType());
            }
        }

        protected static ILog GetLog(Type t)
        {
            return LogManager.GetLogger(t);
        }

        protected static ILog GetLog()
        {
            //return LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType); //有问题，子类调用，返回的还是父类的logger

            var trace = new System.Diagnostics.StackTrace();
            Type baseType = typeof(BaseController);
            for (int i = 0; i < trace.FrameCount; i++)
            {
                var frame = trace.GetFrame(i);
                var method = frame.GetMethod();
                var type = method.DeclaringType;
                if (type.IsSubclassOf(baseType)) return GetLog(type);
            }
            return LogManager.GetLogger(baseType);
        }

        protected void SetTrackID()
        {
            if (ThreadContext.Properties["trackid"] == null)
                ThreadContext.Properties["trackid"] = DateTime.Now.TimeOfDay.TotalMilliseconds.ToString("00000000"); //Guid.NewGuid().ToString("N");
        }

        public ManagerController()
        {
            _currentDb = new LumosDbContext();
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);


        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                CurrentDb.SysPageAccessRecord.Add(new SysPageAccessRecord() { UserId = User.Identity.GetUserId<int>(), AccessTime = DateTime.Now, PageUrl = filterContext.HttpContext.Request.Url.AbsolutePath, Ip = CommonUtils.GetIP() });
                CurrentDb.SaveChanges();
            }

            ILog log = LogManager.GetLogger(CommonSetting.LoggerAccessWeb);
            log.Info(FormatUtils.AccessWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));


            //if (filterContext.HttpContext.Request.Url.AbsolutePath.IndexOf(ManagerUtils.GetLoginPage()) == -1)
            //{
            //    if (Request.IsAuthenticated)
            //    {
            //        var userId = User.Identity.GetUserId<int>();
            //        var user = CurrentDb.SysStaffUser.Where(m => m.Id == userId).FirstOrDefault();
            //        if (user == null)
            //        {
            //            Response.Redirect(ManagerUtils.GetLoginPage());
            //        }
            //    }
            //}

        }

        #region 上传文件
        public string GetUploadFilePath(string fileinputname, string savepath, string oldFileName)
        {
            string savefilePath = null;
            try
            {
                if (fileinputname == null)
                {
                    return null;
                }
                if (fileinputname.Trim() == "")
                {
                    return null;
                }

                if (savepath == null)
                {
                    return null;
                }


                if (savepath.Trim() == "")
                {
                    return null;
                }

                HttpPostedFileBase file_upload = Request.Files[fileinputname];

                if (file_upload == null)
                    return null;
                if (file_upload.ContentLength == 0)
                    return null;

                string savePath = savepath;
                string extension = System.IO.Path.GetExtension(file_upload.FileName).ToLower();
                string newfilename = DateTime.Now.ToString("yyyyMMddhhmmssfff");//新文件名称
                newfilename = newfilename + extension;
                string filePath = "";
                savefilePath = filePath = string.Format("{0}/{1}", savePath, newfilename);
                filePath = System.Web.HttpContext.Current.Server.MapPath("~/") + filePath;
                DirectoryInfo dir = new DirectoryInfo(System.Web.HttpContext.Current.Server.MapPath(savePath) + "/");
                if (!dir.Exists) { dir.Create(); }
                if (file_upload != null)
                {
                    file_upload.SaveAs(filePath);

                    if (oldFileName != null)
                    {
                        DelFile(oldFileName);
                    }
                }

                return savefilePath;
            }
            catch
            {
                return null;
            }
        }

        private bool DelFile(string fileName)
        {
            bool isflag = false;
            string delPath = System.Web.HttpContext.Current.Server.MapPath("~/");
            string delPicPath = string.Empty;
            int deleteCount = 0;
            try
            {
                delPicPath = string.Format("{0}/{1}", delPath, fileName);
                if (System.IO.File.Exists(delPicPath))//如果该文件存在，则删除
                {
                    deleteCount++;
                    System.IO.File.Delete(delPicPath);
                    isflag = true;
                }
            }
            catch
            {
                if (deleteCount < 100)//为避免因资源被占用删除不了数据   所以在此循环100次
                {
                    DelFile(fileName);
                }
            }
            return isflag;
        }
        #endregion
    }
}