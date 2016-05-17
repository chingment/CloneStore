using Lumos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebSite.Areas.Manager
{
    public static class ManagerUtils
    {
        /// <summary>
        /// 获取登录的页面
        /// </summary>
        /// <returns></returns>
        public static string GetLoginPage()
        {
            return "/Manager/Home/Login";
        }

        /// <summary>
        /// 获取登录后的主界面
        /// </summary>
        /// <returns></returns>
        public static string GetHomePage()
        {
            return "/Manager/Home/Index";
        }

        public static bool CanViewErrorStackTrace()
        {
            string[] canViewIp = new string[] { "127.0.0.1", "::1" };


            string ip = CommonUtils.GetIP();

            if(canViewIp.Contains(ip))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}