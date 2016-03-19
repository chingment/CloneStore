using Lumos.Common;
using Lumos.DAL;
using Lumos.DAL.AuthorizeRelay;
using Lumos.Entity;
using log4net;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebSite.Areas.Manager.Models;

namespace WebSite.Areas.Manager.Controllers
{

    public class ProvinceCityArea
    {
        public string Id { get; set; }
        public string cityId { get; set; }
        public string cityName { get; set; }
        public string areaName { get; set; }
        public string provinceId { get; set; }
    }

    public class HomeController : ManagerBaseController
    {
        /// <summary>  
        /// 获取客户端Ip  
        /// </summary>  
        /// <returns></returns>  
        public String GetClientIP()
        {
            String clientIP = "";
            if (System.Web.HttpContext.Current != null)
            {
                clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(clientIP) || (clientIP.ToLower() == "unknown"))
                {
                    clientIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_REAL_IP"];
                    if (string.IsNullOrEmpty(clientIP))
                    {
                        clientIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                    }
                }
                else
                {
                    clientIP = clientIP.Split(',')[0];
                }
            }
            return clientIP;
        }



        public ViewResult Index()
        {
            return View();
        }

        public ViewResult Main()
        {
            return View();
        }

        protected string CreateKey(int len)
        {

            byte[] bytes = new byte[len];

            new RNGCryptoServiceProvider().GetBytes(bytes);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < bytes.Length; i++)
            {

                sb.Append(string.Format("{0:X2}", bytes[i]));

            }

            return sb.ToString();

        }


        [AllowAnonymous]
        public ViewResult Login()
        {
            #region 省份 区域代码
            //string sql_hotCity = "select * from SysProvinceCity where pid IN( select id from [dbo].[SysProvinceCity] where PId='1')";
            //var hotCity = CurrentDb.Database.SqlQuery<SysProvinceCity>(sql_hotCity).ToList();
            //StringBuilder sb = new StringBuilder();
            //if (hotCity != null)
            //{
            //    sb.Append("{");
            //    sb.Append("\"success\": true,");
            //    sb.Append("\"isException\": false,");
            //    sb.Append("\"area\": null,");
            //    sb.Append("\"cities\": [");
            //    foreach (SysProvinceCity m in hotCity)
            //    {
            //        sb.Append("{");
            //        sb.Append("\"name\":\"" + m.Name + "\",");
            //        sb.Append("\"id\": \"" + m.Id + "\",");
            //        sb.Append("\"cityPinyin\": \"\",");
            //        sb.Append("\"lastModifyTime\": null,");
            //        sb.Append("\"provinceId\": \"" + m.PId + "\",");
            //        string ishotCity = "false";
            //        string[] hotCitys = new string[] { "1101", "1201", "3101", "3205", "3302", "4401", "4403", "4419", "5101", "8101", "9101" };
            //        if (hotCitys.Contains(m.Id))
            //        {
            //            ishotCity = "true";
            //        }
            //        sb.Append("\"hotCity\": " + ishotCity + ",");
            //        sb.Append("\"cityShortPY\": \"\"");
            //        sb.Append("},");
            //    }
            //    if (hotCity.Count > 0)
            //    {
            //        sb.Remove(sb.Length - 1, 1);
            //    }
            //    sb.Append("],");
            //    sb.Append("\"city\": null,");
            //    sb.Append("\"cityId\": null,");
            //    sb.Append("\"exception\": false,");
            //    sb.Append("\"jiageCities\": null,");
            //    sb.Append("\"proId\": null,");
            //    sb.Append("\"successResultValue\": null");
            //    sb.Append("}");

            //    string hotCityJson = sb.ToString();
            //}

            //string provinceJson = sb.ToString();


            //string sql_Province = " select * from SysProvinceCity where Pid='1' ";
            //var provinces = CurrentDb.Database.SqlQuery<SysProvinceCity>(sql_Province).ToList();
            //StringBuilder sb = new StringBuilder();
            //if (provinces != null)
            //{
            //    sb.Append("{");
            //    sb.Append("\"success\": true,");
            //    sb.Append("\"isException\": false,");
            //    sb.Append("\"exception\": false,");
            //    sb.Append("\"successResultValue\": null,");
            //    sb.Append("\"province\": null,");
            //    sb.Append("\"provinceId\": null,");
            //    sb.Append("\"provinces\": [");
            //    foreach (SysProvinceCity m in provinces)
            //    {
            //        sb.Append("{");
            //        sb.Append("\"id\": \"" + m.Id + "\",");
            //        sb.Append("\"provinceName\":\"" + m.Name + "\",");
            //        sb.Append("\"updateTime\": null,");
            //        sb.Append("\"indexId\": null");
            //        sb.Append("},");
            //    }
            //    if (provinces.Count > 0)
            //    {
            //        sb.Remove(sb.Length - 1, 1);
            //    }
            //    sb.Append("]");
            //    sb.Append("}");

            //    string provinceJson = sb.ToString();
            //}


            //string sql_Area = " select Id,Name as areaName,b.cityId,b.cityName,b.provinceId  from SysProvinceCity  a  inner join (select id as cityId,name as cityName, pid as provinceId from SysProvinceCity where pid IN( select id from [dbo].[SysProvinceCity] where PId='1') ) b on a.PId=b.cityId";
            //var area = CurrentDb.Database.SqlQuery<ProvinceCityArea>(sql_Area).ToList();
            //StringBuilder sb = new StringBuilder();
            //if (area != null)
            //{
            //    sb.Append("{");
            //    sb.Append("\"success\": true,");
            //    sb.Append("\"isException\": false,");
            //    sb.Append("\"exception\": false,");
            //    sb.Append("\"successResultValue\": null,");
            //    sb.Append("\"area\": null,");
            //    sb.Append("\"areaId\": null,");
            //    sb.Append("\"areaName\": null,");
            //    sb.Append("\"areas\": [");
            //    foreach (ProvinceCityArea m in area)
            //    {
            //        sb.Append("{");
            //        sb.Append("\"id\": \"" + m.Id + "\",");
            //        sb.Append("\"cityId\":\"" + m.cityId + "\",");
            //        sb.Append("\"cityName\":\"" + m.cityName + "\",");
            //        sb.Append("\"areaName\":\"" + m.areaName + "\",");
            //        sb.Append("\"provinceId\":\"" + m.provinceId + "\",");
            //        sb.Append("\"updateTime\": null,");
            //        sb.Append("\"pinYin\": \"\",");
            //        sb.Append("\"pinYinChar\": \"\",");
            //        sb.Append("\"isShowWithCity\": 0");
            //        sb.Append("},");
            //    }
            //    if (area.Count > 0)
            //    {
            //        sb.Remove(sb.Length - 1, 1);
            //    }
            //    sb.Append("]");
            //    sb.Append("}");

            //    string provinceJson = sb.ToString();
            //}
            #endregion
            ViewBag.DevVersion = "1.0.0.0";
            return View();
        }
        [AllowAnonymous]
        public ViewResult MessageBox()
        {
            return View();
        }

        /// <summary>
        /// 登录方法
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public JsonResult Login(LoginViewModel model)
        {
            var identity = new AspNetIdentiyAuthorizeRelay<SysStaffUser>(CurrentDb);

            var user = identity.SignIn(model.txt_UserName, model.txt_Password, model.ckb_RememberMe);
            if (user != null)
            {
                if (user.IsDisable)
                    return Json(ResultType.Failure, "The account has been disabled");

                if (!user.IsDelete)
                {
                    ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
                    log.Info(FormatUtility.LoginInWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
                    return Json(ResultType.Success, "Sign In Success");
                }

            }
            return Json(ResultType.Failure, "Incorrect Account or password. Please re-enter！");
        }

        /// <summary>
        /// 退出方法
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            ILog log = LogManager.GetLogger(CommonSetting.LoggerLoginWeb);
            log.Info(FormatUtility.LoginOffWeb(User.Identity.GetUserId<int>(), User.Identity.GetUserName()));
            var identity = new AspNetIdentiyAuthorizeRelay<SysUser>(CurrentDb);
            identity.SignOut();
            return RedirectToAction("Login", "Home");
        }

    }
}