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

    public class CommonController : ManagerController
    {

        public JsonResult UploadImage(string fileinputname, string savepath, string oldfilename)
        {
            CustomJsonResult r = new CustomJsonResult();
            r.ContentType = "text/html";
            try
            {
                HttpPostedFileBase file_upload = Request.Files[fileinputname];

                if (file_upload == null)
                    return Json("text/html", ResultType.Failure, "上传失败");

                System.IO.FileInfo file = new System.IO.FileInfo(file_upload.FileName);
                if (file.Extension != ".jpg" && file.Extension != ".png" && file.Extension != ".gif" && file.Extension != ".bmp")
                {
                    return Json("text/html", ResultType.Failure, "上传的文件不是图片格式(jpg,png,gif,bmp)");
                }

                ImageUpload s = new ImageUpload();
                string domain = System.Configuration.ConfigurationManager.AppSettings["custom:Domain"];
                string imagesServerUrl = CommonSetting.GetUploadPath(savepath);
                if (s.Save(file_upload, domain, imagesServerUrl, ""))
                {
                    r.Content = s;
                }

                r.Result = ResultType.Success;
                r.Message = "上传成功";

            }
            catch (Exception ex)
            {
                r.Result = ResultType.Exception;
                r.Message = "上传图片发生异常";
                throw (ex);

            }
            return r;
        }




        /// <summary>
        /// 获取验证码的图片 使用方式 请求url:/Common/GetImgVerifyCode?name=sessionname
        /// </summary>
        /// <param name="name">代表后台session的名称</param>
        /// <returns>返回一张带数字的图片</returns>
        [AllowAnonymous]
        public ActionResult GetImgVerifyCode(string name)
        {
            VerifyCodeHelper v = new VerifyCodeHelper();
            v.CodeSerial = "0,1,2,3,4,5,6,7,8,9";
            string code = v.CreateVerifyCode(); //取随机码 
            v.CreateImageOnPage(code, ControllerContext.HttpContext);   //输出图片
            Session[name] = code;   //Session 取出验证码
            Response.End();
            return null;
        }
    }
}